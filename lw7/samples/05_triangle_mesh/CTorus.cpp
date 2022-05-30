#include "stdafx.h"
#include "CTorus.h"
#include "Ray.h"
#include "Intersection.h"

CTorus::CTorus(double radius, double smallRadius, CVector3d const& center, CMatrix4d const& transform)
	:CGeometryObjectWithInitialTransformImpl(transform)
	
{
	// Сфера заданного радиуса и с центром в заданной точке получается
	// путем масштабирования и переносы базовой сферы (сфера радиуса 1 с центром в начале координат)
	m_radius = radius;
	m_smallRadius = smallRadius;
	CMatrix4d initialTransform;
	//initialTransform.Translate(center.x, center.y, center.z);
	// Задаем начальную трансформацию базовой сферы
	SetInitialTransform(initialTransform);
}

bool CTorus::Hit(CRay const& ray, CIntersection& intersection)const
{
	CRay invRay = Transform(ray, GetInverseTransform());

	double solutions[4];
	int numSolutions = SolveIntersections(invRay.GetDirection(), invRay.GetStart(), solutions);
	const double HIT_TIME_EPSILON = 1e-8;

	double 	t = 2000.0;
	bool intersected = false;
	for (int i = 0; i < numSolutions; i++)
	{
		if (solutions[i] > HIT_TIME_EPSILON) {
			intersected = true;
			if (solutions[i] < t)
				t = solutions[i];
		}
	}
	if (intersected)
	{
		CVector3d hitPoint = ray.GetPointAtTime(t);
		CVector3d hitPointInObjectSpace = invRay.GetPointAtTime(t);
		// Координаты нормали к точке единичной сферы с центром в начале координат
		// совпадают с координатами данной точки, поэтому просто используем ссылку
		CVector3d const& hitNormalInObjectSpace = hitPointInObjectSpace;
		// Вычисляем нормаль в точке соударения
		CVector3d hitNormal = GetNormalMatrix() * hitNormalInObjectSpace;
		CHitInfo hit(
			t, *this,
			hitPoint, hitPointInObjectSpace,
			hitNormal, hitNormalInObjectSpace
		);
		intersection.AddHit(hit);
	}

	// Было ли хотя бы одно столкновение луча со сферой в положительном времени?
	return numSolutions > 0;
}

int CTorus::SolveIntersections(const CVector3d& direction, const CVector3d& vantage, double uArray[4]) const
{
	double ox = vantage.x;
	double oy = vantage.y;
	double oz = vantage.z;

	double dx = direction.x;
	double dy = direction.y;
	double dz = direction.z;

	// define the coefficients of the quartic equation
	//(x^2 + y^2 + z^2 + R^2 - r^2)^2 - 4R^2 * (x^2 + y^2)=0
	//( x^2 + y^2 + z^2 )^2 − 2^(R^2+r^2) * (x^2+y^2+z^2) + 4R^2 * y^2 + (R^2−r^2)^2 = 0
	double sum_d_sqrd = dx * dx + dy * dy + dz * dz;
	double e = ox * ox + oy * oy + oz * oz -
		m_radius * m_radius - m_smallRadius * m_smallRadius;
	double f = ox * dx + oy * dy + oz * dz;
	double four_a_sqrd = 4.0 * m_radius * m_radius;


	complex croots[4];
	const int numComplexRoots = SolveQuarticEquation(
	complex(sum_d_sqrd * sum_d_sqrd),                    // coefficient of u^4
	complex(4.0 * sum_d_sqrd * f),                // coefficient of u^3
	complex(2.0 * sum_d_sqrd * e + 4.0 * f * f + four_a_sqrd * dy * dy),      // coefficient of u^2
	complex(4.0 * f * e + 2.0 * four_a_sqrd * oy * dy),            // coefficient of u^1 = u
	complex(e * e - four_a_sqrd * (m_smallRadius * m_smallRadius - oy * oy)),                // coefficient of u^0 = constant term
	croots);

	const int numRealRoots = FilterRealNumbers(numComplexRoots, croots, uArray);

	return numRealRoots;
}

bool CTorus::IsZero(complex x) const
{
	return (fabs(x.real()) < EPSILON) && (fabs(x.imag()) < EPSILON);
}

int CTorus::FilterRealNumbers(int numComplexValues, const complex inArray[], double outArray[]) const
{
	int numRealValues = 0;
	for (int i = 0; i < numComplexValues; ++i)
	{
		if (fabs(inArray[i].imag()) < EPSILON)
		{
			outArray[numRealValues++] = inArray[i].real();
		}
	}
	return numRealValues;
}

int CTorus::SolveQuadraticEquation(complex a, complex b, complex c, complex roots[2]) const
{
	if (IsZero(a))
	{
		if (IsZero(b))
		{
			// The equation devolves to: c = 0, where the variable x has vanished!
			return 0;   // cannot divide by zero, so there is no solution.
		}
		else
		{
			// Simple linear equation: bx + c = 0, so x = -c/b.
			roots[0] = -c / b;
			return 1;   // there is a single solution.
		}
	}
	else
	{
		const complex radicand = b * b - 4.0 * a * c;
		if (IsZero(radicand))
		{
			// Both roots have the same value: -b / 2a.
			roots[0] = -b / (2.0 * a);
			return 1;
		}
		else
		{
			// There are two distinct real roots.
			const complex r = sqrt(radicand);
			const complex d = 2.0 * a;

			roots[0] = (-b + r) / d;
			roots[1] = (-b - r) / d;
			return 2;
		}
	}
}

int CTorus::SolveCubicEquation(complex a, complex b, complex c, complex d, complex roots[3]) const
{
	if (IsZero(a))
	{
		return SolveQuadraticEquation(b, c, d, roots);
	}

	b /= a;
	c /= a;
	d /= a;

	complex S = b / 3.0;
	complex D = c / 3.0 - S * S;
	complex E = S * S * S + (d - S * c) / 2.0;
	complex Froot = sqrt(E * E + D * D * D);
	complex F = -Froot - E;

	if (IsZero(F))
	{
		F = Froot - E;
	}

	for (int i = 0; i < 3; ++i)
	{
		const complex G = cbrt(F, i);
		roots[i] = G - D / G - S;
	}

	return 3;
}

int CTorus::SolveQuarticEquation(complex a, complex b, complex c, complex d, complex e, complex roots[4]) const
{
	if (IsZero(a))
	{
		return SolveCubicEquation(b, c, d, e, roots);
	}

	// See "Summary of Ferrari's Method" in http://en.wikipedia.org/wiki/Quartic_function

	// Without loss of generality, we can divide through by 'a'.
	// Anywhere 'a' appears in the equations, we can assume a = 1.
	b /= a;
	c /= a;
	d /= a;
	e /= a;

	complex b2 = b * b;
	complex b3 = b * b2;
	complex b4 = b2 * b2;

	complex alpha = (-3.0 / 8.0) * b2 + c;
	complex beta = b3 / 8.0 - b * c / 2.0 + d;
	complex gamma = (-3.0 / 256.0) * b4 + b2 * c / 16.0 - b * d / 4.0 + e;

	complex alpha2 = alpha * alpha;
	complex t = -b / 4.0;

	if (IsZero(beta))
	{
		complex rad = sqrt(alpha2 - 4.0 * gamma);
		complex r1 = sqrt((-alpha + rad) / 2.0);
		complex r2 = sqrt((-alpha - rad) / 2.0);

		roots[0] = t + r1;
		roots[1] = t - r1;
		roots[2] = t + r2;
		roots[3] = t - r2;
	}
	else
	{
		complex alpha3 = alpha * alpha2;
		complex P = -(alpha2 / 12.0 + gamma);
		complex Q = -alpha3 / 108.0 + alpha * gamma / 3.0 - beta * beta / 8.0;
		complex R = -Q / 2.0 + sqrt(Q * Q / 4.0 + P * P * P / 27.0);
		complex U = cbrt(R, 0);
		complex y = (-5.0 / 6.0) * alpha + U;
		if (IsZero(U))
		{
			y -= cbrt(Q, 0);
		}
		else
		{
			y -= P / (3.0 * U);
		}
		complex W = sqrt(alpha + 2.0 * y);

		complex r1 = sqrt(-(3.0 * alpha + 2.0 * y + 2.0 * beta / W));
		complex r2 = sqrt(-(3.0 * alpha + 2.0 * y - 2.0 * beta / W));

		roots[0] = t + (W - r1) / 2.0;
		roots[1] = t + (W + r1) / 2.0;
		roots[2] = t + (-W - r2) / 2.0;
		roots[3] = t + (-W + r2) / 2.0;
	}

	return 4;
}

CTorus::complex CTorus::cbrt(complex a, int n) const
{
	
	const double TWOPI = 2.0 * 3.141592653589793238462643383279502884;

	double rho = pow(abs(a), 1.0 / 3.0);
	double theta = ((TWOPI * n) + arg(a)) / 3.0;
	return complex(rho * cos(theta), rho * sin(theta));
}
