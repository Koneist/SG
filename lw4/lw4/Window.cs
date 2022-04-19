using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace lw4
{
    internal class Window : GameWindow
    {
        private Figure _figure = new Figure(1f);
        private const float FIELD_OF_VIEW = 60 * MathF.PI / 180.0f;
        // Размер стороны куба
        private const float CUBE_SIZE = 1;

        private const float Z_NEAR = 0.1f;
        private const float Z_FAR = 10;
        private Matrix4 _cameraMatrix = Matrix4.LookAt(new(0, 0, 2),
                                                       new(0, 0, 0),
                                                       new(0, 2, 0));

        public static Window StartWindow(NativeWindowSettings nativeWindowSettings)
        {
            return new Window(GameWindowSettings.Default, nativeWindowSettings);
        }

        public Window(GameWindowSettings gameWindowSettings,
                      NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _figure.SetSideColor(0, Color4.Black);
        }

        protected override void OnLoad()
        {
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.DepthTest);
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
            GL.Viewport(0, 0, e.Width, e.Height);
            float aspect = (float)e.Width / (float)e.Height;
            
            GL.MatrixMode(MatrixMode.Projection);
            var proj = Matrix4.CreatePerspectiveFieldOfView(FIELD_OF_VIEW, aspect, Z_NEAR, Z_FAR);

            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);

            base.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Draw();

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        void Draw()
        {
            GL.ClearColor(Color4.White);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref _cameraMatrix);

            _figure.Draw();
        }
    }
}
