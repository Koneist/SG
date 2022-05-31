#include "stdafx.h"
#include "Equation.h"

bool Equation::IsZero(complex x)
{
    return (fabs(x.real()) < EPSILON) && (fabs(x.imag()) < EPSILON);
}

std::vector<double> Equation::GetRealNumbers(std::vector<complex>& inArray)
{
	int numRealValues = 0;
	std::vector<double> result;
	for (int i = 0; i < inArray.size(); ++i)
	{
		if (fabs(inArray[i].imag()) < EPSILON)
		{
			result.push_back(inArray[i].real());
		}
	}
	return result;
}

std::vector<double> Equation::SolveQuadraticEquation(double a, double b, double c)
{
	if (IsZero(a))
	{
		if (IsZero(b))
			return std::vector<double>();

		return { -c / b };
	}

	const double radicand = b * b - 4.0 * a * c;

	if (IsZero(radicand))
		return { -b / (2.0 * a) };

	const double r = sqrt(radicand);
	const double d = 2.0 * a;

	std::vector<double> roots(2);
	roots[0] = (-b + r) / d;
	roots[1] = (-b - r) / d;
	return roots;
}

std::vector<complex> Equation::SolveCubicEquation(double a, double b, double c, double d)
{
	//if (IsZero(a))
	//	return SolveQuadraticEquation(b, c, d);

	b /= a;
	c /= a;
	d /= a;

	complex S = b / 3.0;
	complex D = c / 3.0 - S * S;
	complex E = S * S * S + (d - S * c) / 2.0;
	complex Froot = sqrt(E * E + D * D * D);
	complex F = -Froot - E;

	if (IsZero(F))
		F = Froot - E;

	std::vector<complex> roots(3);
	for (int i = 0; i < roots.size(); ++i)
	{
		const complex G = cbrt(F, i);
		roots[i] = G - D / G - S;
	}

	return roots;
}

std::vector<complex> Equation::SolveQuarticEquation(double a, double b, double c, double d, double e)
{
	//if (IsZero(a))
	//	return SolveCubicEquation(b, c, d, e);

	b /= a;
	c /= a;
	d /= a;
	e /= a;

	double b2 = b * b;
	double b3 = b * b2;
	double b4 = b2 * b2;

	double alpha = (-3.0 / 8.0) * b2 + c;
	double beta = b3 / 8.0 - b * c / 2.0 + d;
	double gamma = (-3.0 / 256.0) * b4 + b2 * c / 16.0 - b * d / 4.0 + e;

	double alpha2 = alpha * alpha;
	double alpha3 = alpha * alpha2;
	double t = -b / 4.0;

	std::vector<complex> roots(4);
	if (IsZero(beta))
	{
		complex rad = sqrt(alpha2 - 4.0 * gamma);
		complex r1 = sqrt((-alpha + rad) / 2.0);
		complex r2 = sqrt((-alpha - rad) / 2.0);

		roots[0] = t + r1;
		roots[1] = t - r1;
		roots[2] = t + r2;
		roots[3] = t - r2;
		return roots;
	}

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


	return roots;
}

complex Equation::cbrt(complex a, int n)
{
	const double PI_2 = 2.0 * M_PI;

	double rho = pow(abs(a), 1.0 / 3.0);
	double theta = ((PI_2 * n) + arg(a)) / 3.0;
	return complex(rho * cos(theta), rho * sin(theta));
}
