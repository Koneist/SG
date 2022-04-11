using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Shapes
{
    internal class BrokenLine : Shape
    {
        private float[] _vertices;

        public BrokenLine(float[] vertices,
                       Color4 strokeColor,
                       float strokeWidth)
            : base(strokeColor, strokeWidth)
        {
            _vertices = vertices;
        }

        public override void Draw(Window canvas)
        {
            throw new NotImplementedException();
        }
    }
}
