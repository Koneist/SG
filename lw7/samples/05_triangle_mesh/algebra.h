/*
    algebra.h

    Copyright (C) 2013 by Don Cross  -  http://cosinekitty.com/raytrace

    This software is provided 'as-is', without any express or implied
    warranty. In no event will the author be held liable for any damages
    arising from the use of this software.

    Permission is granted to anyone to use this software for any purpose,
    including commercial applications, and to alter it and redistribute it
    freely, subject to the following restrictions:

    1. The origin of this software must not be misrepresented; you must not
       claim that you wrote the original software. If you use this software
       in a product, an acknowledgment in the product documentation would be
       appreciated but is not required.

    2. Altered source versions must be plainly marked as such, and must not be
       misrepresented as being the original software.

    3. This notice may not be removed or altered from any source
       distribution.

    -------------------------------------------------------------------------

    Solves algebraic problems for linear systems of 3 equations in 3 unknowns,
    and quadratic, cubic, and quartic equations of one variable.
*/
#include <complex>
#include "stdafx.h"

typedef std::complex<double> complex;

const double EPSILON = 1.0e-8;

inline bool IsZero(complex x)
{
    return (fabs(x.real()) < EPSILON) && (fabs(x.imag()) < EPSILON);
}
//
//   Solves the linear system of equations for the variables u,v,w:
//
//       Du + Ev + Fw + G = 0
//       Hu + Iv + Jw + K = 0
//       Lu + Mv + Nw + P = 0
//
//   Where D..P are known real numbers.
//
//   If a solution is possible, returns true and sets u, v, w.
//   If no solution exists, returns false and leaves u, v, w unmodified.
//
bool SolveLinearEquations(
    // input parameters
    double D, double E, double F, double G,
    double H, double I, double J, double K,
    double L, double M, double N, double P,

    // output parameters (set only if this function returns true)
    double& u, double& v, double& w)
{
    // The intermediate variable names reflect how I solved the linear equations by hand with pencil and paper.
    // But we actually calculate their values in somewhat non-alphabetical order
    // because that minimizes the CPU time in case we have to return false.
    // In other words, we do the least amount of work needed to figure out whether
    // there might not be a solution, before bothering to run all the calculations.
    // Also, we try to help branch-prediction of most compilers by assuming that most of the time
    // the solution does exist: the 'if' conditions are assumed to be true more often than false,
    // minimizing branch prediction failures.

    if (fabs(F) >= EPSILON)      // avoid dividing by zero (or something too close to zero)
    {
        const double b = E * J - F * I;
        if (fabs(b) >= EPSILON)      // avoid dividing by zero (or something too close to zero)
        {
            const double a = D * J - F * H;
            const double d = H * N - J * L;
            const double e = I * N - J * M;
            const double denom = a * e - b * d;
            if (fabs(denom) >= EPSILON)      // avoid dividing by zero (or something too close to zero)
            {
                // A solution exists, so set all the output parameters and return true.
                const double c = G * J - F * K;
                const double f = K * N - J * P;

                u = (b * f - e * c) / denom;
                v = -(a * u + c) / b;
                w = -(D * u + E * v + G) / F;

                return true;      // Inform the caller that a solution was found, and (u, v, w) are valid.
            }
        }
    }

    return false;   // No solution exists.  The caller must *NOT* use the values (u, v, w) -- they contain random garbage!
}

// Returns n=0..numComplexValues, and fills in outArray with the n values from
// inArray that are real-valued (i.e., whose imaginary parts are within TOLERANCE of 0.)
// outArray must be large enough to receive numComplexValues values.
int GetRealNumbers(int numComplexValues, const complex inArray[], double outArray[])
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

// Returns n=0..2, the number of distinct real roots found for the equation
//
//     ax^2 + bx + c = 0
//
// Stores the roots in the first n slots of the array 'roots'.
int SolveQuadraticEquation(complex a, complex b, complex c, complex roots[2])
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

complex cbrt(complex a, int n)
{
    /*
        This function returns one of the 3 complex cube roots of the complex number 'a'.
        The value of n=0..2 selects which root is returned.
    */

    const double TWOPI = 2.0 * 3.141592653589793238462643383279502884;

    double rho = pow(abs(a), 1.0 / 3.0);
    double theta = ((TWOPI * n) + arg(a)) / 3.0;
    return complex(rho * cos(theta), rho * sin(theta));
}

// Returns n=0..3, the number of distinct real roots found for the equation
//
//     ax^3 + bx^2 + cx + d = 0
//
// Stores the roots in the first n slots of the array 'roots'.
int SolveCubicEquation(complex a, complex b, complex c, complex d, complex roots[3])
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

// Returns n=0..4, the number of distinct real roots found for the equation
//
//     ax^4 + bx^3 + cx^2 + dx + e = 0
//
// Stores the roots in the first n slots of the array 'roots'.
int SolveQuarticEquation(complex a, complex b, complex c, complex d, complex e, complex roots[4])
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

class SolverException
{
public:
    SolverException(const char* _message)
        : message(_message)
    {
    }

    const char* GetMessage() const { return message; }

private:
    const char* message;
};