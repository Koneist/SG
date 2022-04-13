using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Shapes
{
    internal class Ellipse : SolidShape
    {
        private float _radiusX;
        private float _radiusY;
        private float _centerX;
        private float _centerY;
        public Ellipse(float centerX,
                       float centerY,
                       float radiusX,
                       float radiusY,
                       Color4 fillColor,
                       Color4? strokeColor = null,
                       float strokeWidth = 1)
            : base(fillColor, strokeColor, strokeWidth)
        {
            _radiusX = radiusX;
            _radiusY = radiusY;
            _centerX = centerX;
            _centerY = centerY;
        }

        public override void Draw(Window canvas)
        {
            canvas.DrawEllipse(_centerX, _centerY, _radiusX, _radiusY, _fillColor, _strokeColor, _strokeWidth);
        }
    }
}
