using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace lw5.Object
{
    internal class Wall : gameObject
    {
        private float _width;
        private float _length;
        private float _height;
        private string _textureName;
        private Texture _texture;
        private bool _isLoad = false;

        private float[] _vertexBuffer;

        private float[] _texCoordBuffer = 
        {
            0, 0,
            0, 1,
            1, 1,
            1, 0,
            0, 0,
            0, 1,
            1, 1,
            1, 0,
            0, 0,
            0, 1,
            1, 1,
            1, 0,
            0, 0,
            0, 1,
            1, 1,
            1, 0,
            0, 0,
            0, 1,
            1, 1,
            1, 0,
            0, 0,
            0, 1,
            1, 1,
            1, 0,

        };

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
            new[] { 5, 1, 2, 6 },
            new[] { 4, 0, 1, 5 },
            new[] { 7, 6, 2, 3 },
            new[] { 0, 3, 2, 1 },
            new[] { 4, 5, 6, 7 },
        };

        public Wall(Vector3 pos, float width, float length, float height, string textureName)
            : base(pos)
        {
            _width = width;
            _length = length;
            _height = height;
            _textureName = textureName;
            //_texture = Texture.LoadFromFile(_textureName);

            SetBuffer();
        }

        private void SetBuffer()
        {
            List<float> vertexBuffer = new();

            foreach(var face in _faces)
            {
                foreach(var vertex in face)
                {
                    var vec = _baseVerteces[vertex];
                    vertexBuffer.Add(vec.X * _length * 0.5f);
                    vertexBuffer.Add(vec.Y * _width * 0.5f);
                    vertexBuffer.Add(vec.Z * _height * 0.5f);
                }
            }

            _vertexBuffer = vertexBuffer.ToArray();
        }

        public void Draw()
        {
            if (!_isLoad)
            {
                _texture = Texture.LoadFromFile(_textureName);
                _isLoad = true;
            }

            GL.PushMatrix();

            GL.Enable(EnableCap.Texture2D);
            _texture.Use(TextureUnit.Texture0);

            GL.Translate(Position);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, _vertexBuffer);

            //GL.EnableClientState(ArrayCap.NormalArray);
            //GL.NormalPointer(NormalPointerType.Float, 0, _normalBuffer);

            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, _texCoordBuffer);
            GL.DrawArrays(PrimitiveType.Quads, 0, _vertexBuffer.Length / 3);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);

            GL.PopMatrix();
            //GL.DisableClientState(ArrayCap.NormalArray);
        }
    }
}
