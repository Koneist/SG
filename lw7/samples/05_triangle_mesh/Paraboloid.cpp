#include "stdafx.h"
#include "Paraboloid.h"
#include "Equation.h"

CParaboloid::CParaboloid(CMatrix4d const& transform)
	: CGeometryObjectWithInitialTransformImpl(transform)
{
	CMatrix4d initialTransform;
	SetInitialTransform(initialTransform);
}

bool CParaboloid::Hit(CRay const& ray, CIntersection& intersection) const
{
	enum HitSurface
	{
		HIT_SIDE,
		HIT_CAP,
	};

	struct HitPoint
	{
		double hitTime;
		HitSurface hitSurface;
	};

	unsigned numHits = 0;
	HitPoint hitPoints[2];
	
	CRay invRay = Transform(ray, GetInverseTransform());
	const double HIT_TIME_EPSILON = 1e-8;

	double ox = invRay.GetStart().x;
	double oy = invRay.GetStart().y;
	double oz = invRay.GetStart().z;

	double dx = invRay.GetDirection().x;
	double dy = invRay.GetDirection().y;
	double dz = invRay.GetDirection().z;

	double a = dx * dx + dz * dz;
	double b = 2 * ox * dx + 2 * oz * dz - dy;
	double c = ox * ox + oz * oz - oy;

	std::vector<double> roots = Equation::SolveQuadraticEquation(a, b, c);

	assert(roots.size() <= 2);
	
	for (auto t : roots)
	{
		double hitY = oy + dy * t;
		if (t > HIT_TIME_EPSILON && hitY <= 1)
		{
			HitPoint hit = { t, HIT_SIDE };
			hitPoints[numHits++] = hit;
		}
	}

	if ((numHits < 2) && (std::abs(dy) > HIT_TIME_EPSILON))
	{
		double t = (1 - oy) / dy;
		if (t > HIT_TIME_EPSILON)
		{
			double hitX = ox + dx * t;
			double hitZ = oz + dz * t;
			if (Sqr(hitX) + Sqr(hitZ) < 1)
			{
				HitPoint hit = { t, HIT_CAP };
				hitPoints[numHits++] = hit;
			}
		}
	}

	if (numHits == 0)
		return false;

	if (numHits == 2)
	{
		if (hitPoints[0].hitTime > hitPoints[1].hitTime)
		{
			// std::swap не получаетс€ использовать из-за особенностей gcc
			// в котором шаблон не имеет доступа к типам, объ€вленным внутри функции
			HitPoint tmp(hitPoints[0]);
			hitPoints[0] = hitPoints[1];
			hitPoints[1] = tmp;
		}
	}

	for (unsigned i = 0; i < numHits; ++i)
	{
		HitPoint const& h = hitPoints[i];

		double const& hitTime = h.hitTime;

		CVector3d hitPoint = ray.GetPointAtTime(hitTime);
		CVector3d hitPointInObjectSpace = invRay.GetPointAtTime(hitTime);
		CVector3d hitNormalInObjectSpace;

		if (h.hitSurface == HIT_SIDE)
			hitNormalInObjectSpace = hitPointInObjectSpace;
		else
			hitNormalInObjectSpace = CVector3d(0, 1, 0);

		CVector3d hitNormal = GetNormalMatrix() * hitNormalInObjectSpace;

		CHitInfo hit(
			hitTime, *this,
			hitPoint, hitPointInObjectSpace,
			hitNormal, hitNormalInObjectSpace
		);

		intersection.AddHit(hit);
	}

	return intersection.GetHitsCount() > 0;
}
