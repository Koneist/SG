using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace lw6.Object
{
	internal class Sphere
	{
		private int _displayList;
		private float _radius;
		private int _slices;
		private int _stacks;
		private float[] _vertexBuffer;
		private float[] _normalBuffer;
		private float[] _texCoordBuffer;


		public Sphere(float radius, int slices, int stacks)
		{
			_radius = radius;
			_slices = slices;
			_stacks = stacks;
			SetSphere();
		}

		public void Draw()
		{
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.VertexPointer(3, VertexPointerType.Float, 0, _vertexBuffer);

			GL.EnableClientState(ArrayCap.NormalArray);
			GL.NormalPointer(NormalPointerType.Float, 0, _normalBuffer);

			GL.EnableClientState(ArrayCap.TextureCoordArray);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, _texCoordBuffer);

			GL.DrawArrays(PrimitiveType.TriangleStrip, 0, _vertexBuffer.Length / 3);

			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.NormalArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);

		}

		private void SetSphere()
		{
			float stackStep = MathF.PI / _stacks;

			float sliceStep = 2 * MathF.PI / _slices;

			List<float> vertexBuffer = new();
			List<float> normalbuffer = new();
			List<float> texCoordBuffer = new();

			for (int stack = 0; stack < _stacks; ++stack)
			{
				float stackAngle = MathF.PI * 0.5f - stack * stackStep;
				float nextStackAngle = stackAngle - stackStep;

				float stackRadius = _radius * MathF.Cos(stackAngle);
				float nextStackRadius = _radius * MathF.Cos(nextStackAngle);
				float z0 = _radius * MathF.Sin(stackAngle);
				float z1 = _radius * MathF.Sin(nextStackAngle);

				// цикл по меридианам
				for (int slice = 0; slice <= _slices; ++slice)
				{
					// вычисляем угол, текущего меридиана
					float sliceAngle = (slice != _slices) ? slice * sliceStep : 0;

					// Вычисляем координаты на текущей параллели
					float x0 = stackRadius * MathF.Cos(sliceAngle);
					float y0 = stackRadius * MathF.Sin(sliceAngle);
					// вычисляем и задаем вектор нормали, текстурные координаты 
					// и положение вершины в пространстве
					Vector3 normal0 = new(x0, y0, z0);
					normal0.Normalize();
					AddVertex(normalbuffer, normal0);
					texCoordBuffer.Add((float)slice / _slices);  //glTexCoord2f(float(slice) / m_slices, float(stack) / m_stacks);
					texCoordBuffer.Add((float)stack / _stacks);

					vertexBuffer.Add(x0);
					vertexBuffer.Add(y0);
					vertexBuffer.Add(z0);

					float x1 = nextStackRadius * MathF.Cos(sliceAngle);
					float y1 = nextStackRadius * MathF.Sin(sliceAngle);
					Vector3 normal1 = new(x1, y1, z1);
					normal1.Normalize();
					AddVertex(normalbuffer, normal1);

					texCoordBuffer.Add((float)slice / _slices);
					texCoordBuffer.Add((float)(stack + 1) / _stacks);
					//glTexCoord2f(float(slice) / m_slices, float(stack + 1) / m_stacks);
					vertexBuffer.Add(x1);
					vertexBuffer.Add(y1);
					vertexBuffer.Add(z1);

				}
			}

			_vertexBuffer = vertexBuffer.ToArray();
			_normalBuffer = normalbuffer.ToArray();
			_texCoordBuffer = texCoordBuffer.ToArray();
		}
		private void AddVertex(List<float> buffer, Vector3 vertex)
		{
			buffer.Add(vertex.X);
			buffer.Add(vertex.Y);
			buffer.Add(vertex.Z);
		}
	}
}
