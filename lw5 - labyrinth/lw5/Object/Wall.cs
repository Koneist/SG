using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace lw5.Object
{
    internal class Wall : GameObject
    {
        private float _width;
        private float _length;
        private float _height;
        private Texture _texture;

        private float[] _vertexBuffer;


        /*

           Y
           |
           |
           |
           +---X
          /
         /
        Z
           3----2
          /    /|
         /    / |
        7----6  |
        |  0 |  1
        |    | /
        |    |/
        4----5
        */


        private List<Vector3> _baseVerteces = new()
        {
            new(-1, -1, -1),          // 0
            new(+1, -1, -1),          // 1
            new(+1, +1, -1),          // 2
            new(-1, +1, -1),          // 3
            new(-1, -1, +1),          // 4
            new(+1, -1, +1),          // 5
            new(+1, +1, +1),          // 6
            new(-1, +1, +1),          // 7
        };

        private List<int[]> _faces = new()
        {
            new[] { 4, 7, 3, 0 },
            new[] { 1, 2, 6, 5 },
            new[] { 4, 0, 1, 5 },
            new[] { 6, 2, 3, 7 },
            new[] { 0, 3, 2, 1 },
            new[] { 5, 6, 7, 4 },
        };
        
        private float[] _texCoordBuffer = 
        {
            1, 0,
            1, 1,
            0, 1,
            0, 0,

            1, 0,
            1, 1,
            0, 1,
            0, 0,

            1, 0,
            1, 1,
            0, 1,
            0, 0,

            1, 0,
            1, 1,
            0, 1,
            0, 0,

            1, 0,
            1, 1,
            0, 1,
            0, 0,

            1, 0,
            1, 1,
            0, 1,
            0, 0,
        };

        public Wall(Vector3 pos, float width, float length, float height, Texture texture)
        : base(pos, length, width, height)
        {
            _width = width;
            _length = length;
            _height = height;
            _texture = texture;

            SetBuffer();
        }

        private void SetBuffer()
        {
            List<float> vertexBuffer = new();

            for(int faceIndex = 0; faceIndex < _faces.Count; ++faceIndex)
            {
                foreach(var vertex in _faces[faceIndex])
                {
                    var vec = _baseVerteces[vertex];
                    vertexBuffer.Add(vec.X * _length * 0.5f);
                    vertexBuffer.Add(vec.Y * _height * 0.5f);
                    vertexBuffer.Add(vec.Z * _width * 0.5f);
                }

                if(faceIndex < 2)
                    SetTexCoord(faceIndex, _width, _height);
                else if (faceIndex == 2 || faceIndex == 3)
                    SetTexCoord(faceIndex, _length, _width);
                else if(faceIndex > 2)
                    SetTexCoord(faceIndex, _length, _height);
            }

            _vertexBuffer = vertexBuffer.ToArray();
        }

        private void SetTexCoord(int faceIndex, float x, float y)
        {
            for (int i = 0; i < 8; i += 2)
            {
                var texVertex = faceIndex * 8;
                _texCoordBuffer[texVertex + i] *= x;
                _texCoordBuffer[texVertex + i + 1] *= y;
            }
        }

        public void Draw()
        {

            GL.PushMatrix();

            GL.Enable(EnableCap.Texture2D);
            _texture.Use(TextureUnit.Texture0);

            GL.Translate(Position);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, _vertexBuffer);


            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, _texCoordBuffer);
            GL.DrawArrays(PrimitiveType.Quads, 0, _vertexBuffer.Length / 3);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);

            GL.PopMatrix();
        }
    }
}
