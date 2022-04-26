using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw4
{
    internal static class Matrix4Ext
    {
        public static Matrix4 Orthonormalize(this Matrix4 m)
        {
            Matrix3 normalizedMatrix = new Matrix3(m).Orthonormalize();
            Matrix4 result = new Matrix4(normalizedMatrix);
            result.Row3 = m.Row3;
            result.Column3 = m.Column3;
            return result;
        }

    }
}
