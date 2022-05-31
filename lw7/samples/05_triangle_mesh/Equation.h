#pragma once
#include "Matrix4.h"
#include <complex>
#include <vector>
#define _USE_MATH_DEFINES
#include <math.h>

typedef std::complex<double> complex;

static class  Equation
{
public:

	static bool IsZero(complex x);
	static std::vector<double> GetRealNumbers(std::vector<complex>& inArray);
	static std::vector<double> SolveQuadraticEquation(double a, double b, double c);
	static std::vector<complex> SolveCubicEquation(double a, double b, double c, double d);
	static std::vector<complex> SolveQuarticEquation(double a, double b, double c, double d, double e);
	static complex cbrt(complex a, int n) ;

private:
	 inline static const double EPSILON = 1.0e-8;

};