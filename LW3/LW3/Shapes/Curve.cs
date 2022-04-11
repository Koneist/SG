using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Shapes
{
    internal class Curve : SolidShape
    {
        private float[] _vertices;

        public Curve(float[] vertices,
                       Color4 fillColor,
                       Color4 strokeColor,
                       float strokeWidth)
            : base(fillColor, strokeColor, strokeWidth)
        {
            _vertices = vertices;
        }

        public override void Draw(Window canvas)
        {
            throw new NotImplementedException();
        }
    }
}
