#pragma once
#include "GeometryObjectWithInitialTransformImpl.h"
#include "Ray.h"
#include "Intersection.h"
#include <vector>
#include <complex>

class CParaboloid :
	public CGeometryObjectWithInitialTransformImpl
{
public:
	CParaboloid(CMatrix4d const& transform = CMatrix4d());

	virtual bool Hit(CRay const& ray, CIntersection& intersection)const;
private:
	typedef std::complex<double> complex;

	//std::vector<double> GetIntersections(const CVector3d& direction, const CVector3d& vantage) const;
	//bool IsZero(complex x) const;
	//std::vector<double> GetRealNumbers(std::vector<complex>& inArray) const;
	//std::vector<complex> SolveQuadraticEquation(double a, double b, double c) const;
	//std::vector<complex> SolveCubicEquation(double a, double b, double c, double d) const;
	//std::vector<complex> SolveQuarticEquation(double a, double b, double c, double d, double e) const;
	//complex cbrt(complex a, int n) const;


private:
	//double m_radius = 1.0;
	//double m_smallRadius = 0.5;
};