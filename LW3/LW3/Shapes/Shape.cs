using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Shapes
{
    internal abstract class Shape : IDrawable
    {
        protected Color4 _strokeColor = Color4.Transparent;
        protected float _strokeWidth = 0;

        protected Shape(Color4 strokeColor, float width)
        {
            _strokeColor = strokeColor;
            _strokeWidth = width;
        }

        public Color4 StrokeColor { get => _strokeColor; }

        public abstract void Draw(Window canvas);
    }
}
