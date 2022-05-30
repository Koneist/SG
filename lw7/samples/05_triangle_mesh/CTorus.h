#pragma once
#include "GeometryObjectWithInitialTransformImpl.h"
#include <complex>

class CTorus :
	public CGeometryObjectWithInitialTransformImpl
{
public:
	CTorus(
		double radius = 1,
		double smallRadius = 0.5,
		CVector3d const& center = CVector3d(),
		CMatrix4d const& transform = CMatrix4d());

	virtual bool Hit(CRay const& ray, CIntersection& intersection)const;
private:
	typedef std::complex<double> complex;
	const double EPSILON = 1.0e-8;

	int SolveIntersections(const CVector3d& direction, const CVector3d& vantage, double uArray[4]) const;
	bool IsZero(complex x) const;
	int FilterRealNumbers(int numComplexValues, const complex inArray[], double outArray[]) const;
	int SolveQuadraticEquation(complex a, complex b, complex c, complex roots[2]) const;
	int SolveCubicEquation(complex a, complex b, complex c, complex d, complex roots[3]) const;
	int SolveQuarticEquation(complex a, complex b, complex c, complex d, complex e, complex roots[4]) const;
	complex cbrt(complex a, int n) const;


private:
	double m_radius = 1.0;
	double m_smallRadius = 0.5;
};