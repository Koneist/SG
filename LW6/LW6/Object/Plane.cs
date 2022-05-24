using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw6.Object
{
    internal class Plane
    {
        private float[] _vertexBuffer;

        public Plane()
        {
            var verteces = new List<float>();
            Vector3 startPos = new(-1, 0, -1);

            for(int i = 0; i < 10; ++i)
            {
                var currPos = startPos;
                currPos.X += 0.1f * i;

                for(int j = 0; j < 10; ++j)
                {
                    
                    verteces.Add(currPos.X);
                    verteces.Add(currPos.Y);
                    verteces.Add(currPos.Z);

                    verteces.Add(currPos.X + 0.1f);
                    verteces.Add(currPos.Y);
                    verteces.Add(currPos.Z);

                    verteces.Add(currPos.X + 0.1f);
                    verteces.Add(currPos.Y);
                    verteces.Add(currPos.Z + 0.1f);

                    verteces.Add(currPos.X);
                    verteces.Add(currPos.Y);
                    verteces.Add(currPos.Z + 0.1f);

                    currPos.Z += 0.1f;
                }
                
            }
            _vertexBuffer = verteces.ToArray();
        }

        public void Draw()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, _vertexBuffer);

            GL.Color4(Color4.Red);
            GL.DrawArrays(PrimitiveType.Quads, 0, _vertexBuffer.Length / 3);

            GL.DisableClientState(ArrayCap.VertexArray);

            
        }
    }
}
