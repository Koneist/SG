using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Shapes
{
    internal abstract class SolidShape : Shape
    {
        protected Color4 _fillColor = Color4.Transparent;


        protected SolidShape(Color4 fillColor, Color4? strokeColor = null, float strokeWidth = 1)
            : base(strokeColor == null ? Color4.Transparent : strokeColor.Value, strokeWidth)
        {
            _fillColor = fillColor;
        }

        public Color4 FillColor { get => _fillColor; }
    }
}
