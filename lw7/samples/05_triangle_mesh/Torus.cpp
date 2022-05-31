#include "stdafx.h"
#include "Torus.h"
#include "Ray.h"
#include "Intersection.h"
#include <vector>
#include "Equation.h"
#define _USE_MATH_DEFINES
#include <math.h>

CTorus::CTorus(double radius, double smallRadius, CMatrix4d const& transform)
	:CGeometryObjectWithInitialTransformImpl(transform)
	
{
	assert(radius >= 0);
	assert(smallRadius >= 0);

	m_radius = radius;
	m_smallRadius = smallRadius;
	CMatrix4d initialTransform;
	SetInitialTransform(initialTransform);
}

bool CTorus::Hit(CRay const& ray, CIntersection& intersection)const
{
	CRay invRay = Transform(ray, GetInverseTransform());

	std::vector<double> solutions = GetIntersections(invRay.GetDirection(), invRay.GetStart());
	const double HIT_TIME_EPSILON = 1e-8;

	double 	t = DBL_MAX;
	bool intersected = false;
	for (int i = 0; i < solutions.size(); i++)
	{
		if (solutions[i] > HIT_TIME_EPSILON) 
		{
			intersected = true;
			if (solutions[i] < t)
				t = solutions[i];
		}
	}

	if (!intersected)
	{
		return false;
	}

	CVector3d hitPoint = ray.GetPointAtTime(t);
	CVector3d hitPointInObjectSpace = invRay.GetPointAtTime(t);
	CVector3d const& hitNormalInObjectSpace = hitPointInObjectSpace;
	CVector3d hitNormal = GetNormalMatrix() * hitNormalInObjectSpace;
	CHitInfo hit(
		t, *this,
		hitPoint, hitPointInObjectSpace,
		hitNormal, hitNormalInObjectSpace
	);
	intersection.AddHit(hit);

	return true;
}

std::vector<double> CTorus::GetIntersections(const CVector3d& direction, const CVector3d& origin) const
{
	double ox = origin.x;
	double oy = origin.y;
	double oz = origin.z;

	double dx = direction.x;
	double dy = direction.y;
	double dz = direction.z;

	//( x^2 + y^2 + z^2 )^2 − 2^(R^2+r^2) * (x^2+y^2+z^2) + 4R^2 * y^2 + (R^2−r^2)^2 = 0
	double sumDirSqrd = dx * dx + dy * dy + dz * dz;
	double e = ox * ox + oy * oy + oz * oz -
		m_radius * m_radius - m_smallRadius * m_smallRadius;
	double f = ox * dx + oy * dy + oz * dz;
	double fourRadiusSqrd = 4.0 * m_radius * m_radius;

	std::vector<complex> сomplexRoots = Equation::SolveQuarticEquation(
		(sumDirSqrd * sumDirSqrd),												// u^4
		(4.0 * sumDirSqrd * f),													// u^3
		(2.0 * sumDirSqrd * e + 4.0 * f * f + fourRadiusSqrd * dy * dy),		// u^2
		(4.0 * f * e + 2.0 * fourRadiusSqrd * oy * dy),							// u^1
		(e * e - fourRadiusSqrd * (m_smallRadius * m_smallRadius - oy * oy))	// u^0
	);

	return Equation::GetRealNumbers(сomplexRoots);
}
