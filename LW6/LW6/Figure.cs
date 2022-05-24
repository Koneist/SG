using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace lw6
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
        private static readonly float _fi = (1f + MathF.Sqrt(5)) / 2f;
        private static Random _random = new(0);
        private float _size;

        private float[] _vertexBuffer;
        private float[] _colorBuffer;
        private float[] _normalBuffer;
        private Vector4 _specularColor = new(0, 0, 0, 1);
        private float _shininess = 1;

        private List<Vector3> _verteces = new()
        {
            new(-1, -1, -1),          // 0
            new(+1, -1, -1),          // 1
            new(+1, +1, -1),          // 2
            new(-1, +1, -1),          // 3
            new(-1, -1, +1),          // 4
            new(+1, -1, +1),          // 5
            new(+1, +1, +1),          // 6
            new(-1, +1, +1),          // 7
            new(0, +_fi, +(1 / _fi)), // 8 
            new(0, +_fi, -(1 / _fi)), // 9
            new(0, -_fi, +(1 / _fi)), // 10
            new(0, -_fi, -(1 / _fi)), // 11
            new(+(1 / _fi), 0, +_fi), // 12 
            new(+(1 / _fi), 0, -_fi), // 13
            new(-(1 / _fi), 0, +_fi), // 14
            new(-(1 / _fi), 0, -_fi), // 15
            new(+_fi, +(1 / _fi), 0), // 16
            new(+_fi, -(1 / _fi), 0), // 17
            new(-_fi, +(1 / _fi), 0), // 18
            new(-_fi, -(1 / _fi), 0)  // 19
        };

        private List<int[]> _faces = new()
        {
            new [] { 0, 15, 13, 1, 11 }, // 0
            new [] { 0, 11, 10, 4, 19 }, // 1
            new [] { 0, 19, 18, 3, 15 }, // 2
            new [] { 1, 17, 5, 10, 11 }, // 3
            new [] { 1, 13, 2, 16, 17 }, // 4
            new [] { 2, 9, 8, 6, 16 }, // 7
            new [] { 2, 13, 15, 3, 9 }, // 8
            new [] { 3, 18, 7, 8, 9 }, // 11
            new [] { 4, 14, 7, 18, 19 }, // 13
            new [] { 4, 10, 5, 12, 14 }, // 14
            new [] { 5, 17, 16, 6, 12 }, // 16
            new [] { 6, 8, 7, 14, 12 }, // 20 
        };

        public Figure(float size = 1)
        {
            _size = size;

            List<float> verteces = new();
            List<float> normals = new();
            List<float> colors = new();
            foreach (var face in _faces)
            {
                var startVertex = face[0];
                Color4 color = new((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble(), 1);
                for (int i = 2; i < face.Length; ++i)
                {
                    AddVertex(verteces, _verteces[startVertex]);
                    AddVertex(verteces, _verteces[face[i - 1]]);
                    AddVertex(verteces, _verteces[face[i]]);

                    var v0 = _verteces[face[i - 1]] - _verteces[startVertex];
                    var v1 = _verteces[face[i]] - _verteces[startVertex];
                    var normal = Vector3.Cross(v0, v1).Normalized();

                    AddVertex(normals, normal);
                    AddVertex(normals, normal);
                    AddVertex(normals, normal);

                    AddColor(colors, color);
                    AddColor(colors, color);
                    AddColor(colors, color);


                }
            }

            _vertexBuffer = verteces.ToArray();
            _normalBuffer = normals.ToArray();
            _colorBuffer = colors.ToArray();
        }

        private Matrix4 _rotateMatrix = Matrix4.Identity;
        public void Rotate(float angle, Vector3 axis)
        {
            _rotateMatrix *= Matrix4.CreateFromAxisAngle(axis, angle);
        }
        public void Draw()
        {
            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, _specularColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, _shininess);

            GL.PushMatrix();
            GL.MultMatrix(ref _rotateMatrix);
            GL.Scale(_size * 0.5f, _size * 0.5f, _size * 0.5f);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 0, _vertexBuffer);

            GL.EnableClientState(ArrayCap.ColorArray);
            GL.ColorPointer(4, ColorPointerType.Float, 0, _colorBuffer);

            GL.EnableClientState(ArrayCap.NormalArray);
            GL.NormalPointer(NormalPointerType.Float, 0, _normalBuffer);

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexBuffer.Length / 3);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
            GL.DisableClientState(ArrayCap.NormalArray);

            GL.PopMatrix();
        }

        private void AddVertex(List<float> buffer, Vector3 vertex)
        {
            buffer.Add(vertex.X);
            buffer.Add(vertex.Y);
            buffer.Add(vertex.Z);
        }

        private void AddColor(List<float> buffer, Color4 color)
        {
            buffer.Add(color.R);
            buffer.Add(color.G);
            buffer.Add(color.B);
            buffer.Add(color.A);
        }
    }
}
