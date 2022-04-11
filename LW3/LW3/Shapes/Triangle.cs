using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Shapes
{
    internal class Triangle : SolidShape
    {
        public Triangle(Vector2 vertex1,
                        Vector2 vertex2,
                        Vector2 vertex3,
                        Color4 fillColor,
                        Color4 strokeColor,
                        float strokeWidth) 
            : base(fillColor, strokeColor, strokeWidth)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }

        public Vector2 Vertex1 { get; set; }
        public Vector2 Vertex2 { get; set; }
        public Vector2 Vertex3 { get; set; }

        public override void Draw(Window canvas)
        {
            throw new NotImplementedException();
        }
    }
}
