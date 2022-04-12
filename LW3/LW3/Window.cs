using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace LW3
{
    internal class Window : GameWindow
    {
        public static Window StartWindow(NativeWindowSettings nativeWindowSettings)
        {
            return new Window(GameWindowSettings.Default, nativeWindowSettings);
        }

        private float[] _verteces = new float[] 
        {
            -0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f,
             0.5f,  0.5f, 0.0f,
        };
        private int vertecesBuf;

        private BufferObject? _bufObj = null;

        public Window(GameWindowSettings gameWindowSettings,
                      NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.PolygonStipple);
            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
        }

        protected override void OnLoad()
        {
            
            //vertecesBuf = CreateBuffer(_verteces);

            //GL.CullFace(CullFaceMode.Back);
            //GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);
            base.OnLoad();
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }


            base.OnUpdateFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //GL.Viewport(0, 0, Size.X, Size.Y);
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            //GL.Ortho(0, Size.X, Size.Y, 0, -1, 1);
            //GL.MatrixMode(MatrixMode.Modelview);

            GL.Viewport(0, 0, e.Width, e.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();

            GL.Ortho(-e.Width / 2, e.Width / 2, -e.Height / 2, e.Height / 2, -1, 1);

            //DrawBuffer();

            _bufObj?.Draw();

            SwapBuffers();
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //DrawBuffer();
            _bufObj?.Draw();
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        private int CreateBuffer(float[] data)
        {
            int buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, _verteces.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return buffer;
        }

        private void DrawBuffer()
        {
            GL.EnableClientState(ArrayCap.VertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertecesBuf);
            GL.VertexPointer(2, VertexPointerType.Float, 0, 0);

            GL.Color4(Color4.Red);
            GL.Translate(.1f, .1f, 0f);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, (_verteces.Length) / 2);
            //GL.Color4(Color4.Blue);
            //GL.LineWidth(5f);
            //GL.DrawArrays(PrimitiveType.LineStrip, 1, (_verteces.Length - 1) / 2);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);

        }

        public void DrawEllipse(Vector2 center, float rx, float ry, Color4 fillColor, Color4 strokeColor)
        {
            const float points = 24;
            const float step = 2 * MathF.PI / points;
            
            float[] verteces = new float[(int)(2 * (points + 2))];

            verteces[0] = center.X;
            verteces[1] = center.Y;

            for (int point = 0, j = 2; point <= points; point++, j += 2)
            {
                verteces[j] = center.X + rx * MathF.Sin(step * point);
                verteces[j + 1] = center.Y + ry * MathF.Cos(step * point);
            }
            
            _bufObj = new(BufferType.ArrayBuffer);
            _bufObj.SetData(verteces, PrimitiveType.TriangleFan, fillColor);
        }
    }
}
