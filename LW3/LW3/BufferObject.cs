using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace LW3
{
    internal enum BufferType
    {
        ArrayBuffer = BufferTarget.ArrayBuffer,
    }

    internal enum BufferHint
    {
        StaticDraw = BufferUsageHint.StaticDraw,
    }

    internal sealed class BufferObject : IDisposable
    {
        private const int _errorCode = -1;
        private int _dataLength = 0;

        public int BufferId { private get; set; }
        public bool IsActive { private set; get; }
        public Color4 Color { private get; set; } = Color4.Transparent;
        public float StrokeWidth { private set; get; }

        public PrimitiveType PrimitiveType { private set; get; } = PrimitiveType.Points;
        private readonly BufferTarget _type;

        public BufferObject(BufferType type)
        {
            _type = (BufferTarget) type;
            BufferId = GL.GenBuffer();
        }

        public void SetData<T>(T[] data, PrimitiveType primitiveType, Color4 color, float strokeWidth = 1 ,BufferHint hint = BufferHint.StaticDraw ) where T : struct
        {
            if (data.Length == 0)
                throw new ArgumentException("Empty Array");
            Activate();
            GL.BufferData(_type, (IntPtr)(data.Length * Marshal.SizeOf(typeof(T))), data, (BufferUsageHint)hint);

            DeActivate();
            Color = color;
            PrimitiveType = primitiveType;
            _dataLength = data.Length;
            StrokeWidth = strokeWidth;
        }

        public void Draw()
        {
            if (_dataLength == 0 || Color.A == Color4.Transparent.A)
                return;
            
            GL.EnableClientState(ArrayCap.VertexArray);
            if (!IsActive)
                Activate();

            GL.VertexPointer(2, VertexPointerType.Float, 0, 0);
            GL.Color4(Color);
            GL.LineWidth(StrokeWidth);
            GL.DrawArrays(PrimitiveType, 0, _dataLength / 2);
            DeActivate();
            GL.LineWidth(1);
            GL.DisableClientState(ArrayCap.VertexArray);
        }

        public void Activate()
        {
            IsActive = true;
            GL.BindBuffer(_type, BufferId);
        }

        public void DeActivate()
        {
            IsActive = false;
            GL.BindBuffer(_type, 0); 
        }

        public void Delete()
        {
            if (BufferId == _errorCode)
                return;

            DeActivate();
            GL.DeleteBuffer(BufferId);

            BufferId = _errorCode;

        }

        public void Dispose()
        {
            Delete();
            GC.SuppressFinalize(this);
        }
    }
}
