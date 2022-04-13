using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Shapes
{
    internal class Polygon : SolidShape
    {
        private float[] _vertices;

        public Polygon(float[] vertices,
                       Color4 fillColor,
                       Color4? strokeColor = null,
                       float strokeWidth = 1) 
            : base(fillColor, strokeColor, strokeWidth)
        {
            _vertices = vertices;
        }

        public override void Draw(Window canvas)
        {
            canvas.DrawPolygon(_vertices, _fillColor, _strokeColor, _strokeWidth);
        }
    }
}
