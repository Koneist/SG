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
        public float RadiusX { get; set; } = 5;
        public float RadiusY { get; set; } = 5;
        public Vector2 Center { get; set; }
        public Ellipse(Vector2 center, float radiusX, float radiusY, Color4 fillColor, Color4 strokeColor, float strokeWidth = 0)
            : base(fillColor, strokeColor, strokeWidth)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
            Center = center;
        }

        public override void Draw(Window canvas)
        {
            throw new NotImplementedException();
        }
    }
}
