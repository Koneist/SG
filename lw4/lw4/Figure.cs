using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace lw4
{
    enum CubeSide
    {
        NegativeX = 0,
        PositiveX = 1,
        NegativeY = 2,
        PositiveY = 3,
        NegativeZ = 4,
        PositiveZ = 5,
    }
    internal class Figure
    {
        private float _size;
        private Color4 _color;
        private static readonly float[] _verteces = new float[]
        {
            -1, -1, -1, // 0
		    +1, -1, -1, // 1
		    +1, +1, -1, // 2
		    -1, +1, -1, // 3
		    -1, -1, +1, // 4
		    +1, -1, +1, // 5
		    +1, +1, +1, // 6
		    -1, +1, +1, // 7
        };
        // Массив координат вершин
        private static readonly int[] _faces = new int[]
        {
            4, 7, 3, 0, // грань x<0
		    5, 1, 2, 6, // грань x>0
		    4, 0, 1, 5, // грань y<0
		    7, 6, 2, 3, // грань y>0
		    0, 3, 2, 1, // грань z<0
		    4, 5, 6, 7, // грань z>0
        };
        Figure(float size = 1)
        {
            _size = size;
        }
        public void Draw()
        {
            const int faceCount = 6;

            GL.PushMatrix();
            GL.Scale(_size * 0.5f, _size * 0.5f, _size * 0.5f);

            GL.Begin(BeginMode.Quads);
            {
                for(int face = 0; face < faceCount; face += 3)
                {
                    GL.Color4(_color);
                    
                    
                    GL.Vertex3(new Vector3(_verteces[face], _verteces[face + 1], _verteces[face + 2])); 
                    
                }
            }
            GL.End();

            GL.PopMatrix();
        }

        public void SetSideColor(CubeSide side, Color4 color)
        {
            _color = color;
            
        }

    }
}
