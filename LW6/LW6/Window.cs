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
using lw6.Object;

namespace lw6
{
    internal class Window : GameWindow
    {

        private Plane plane = new();

        private const float Z_NEAR = 0.1f;
        private const float Z_FAR = 10;
        private bool _leftMouseBtnPressed = false;
        private Shader _shader;
        private Camera _camera;

        public static Window StartWindow(NativeWindowSettings nativeWindowSettings)
        {
            return new Window(GameWindowSettings.Default, nativeWindowSettings);
        }

        public Window(GameWindowSettings gameWindowSettings,
                      NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.DepthTest);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);


            _camera = new Camera(Vector3.UnitZ * 2, (float)Size.X / (float)Size.Y);
            _shader = new(@"E:\Projects\repository\SG\LW6\LW6\Shaders\shader.vert", @"E:\Projects\repository\SG\LW6\LW6\Shaders\shader.frag");
            _shader.Use();
            base.OnLoad();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!IsFocused)
                return;
            
            var input = KeyboardState;

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.W))
            {
                _camera.MoveForward((float)e.Time);
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.MoveBack((float)e.Time);
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.MoveLeft((float)e.Time);
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.MoveRight((float)e.Time);
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.MoveUp((float)e.Time);
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.MoveDown((float)e.Time);
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = (float)Size.X / (float)Size.Y;

            GL.MatrixMode(MatrixMode.Projection);
            var proj = _camera.GetProjectionMatrix();

            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);

            base.OnResize(e);
        }
        //float time = 0;
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            int timeLocation = GL.GetUniformLocation(_shader.Handle, "uTime");
            GL.Uniform1(timeLocation, 1);

            _shader.Use();

            Draw();

            SwapBuffers();

            base.OnRenderFrame(args);
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButton.Button1)
            {
                _leftMouseBtnPressed = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButton.Button1)
            {
                _leftMouseBtnPressed = false;
            }
        }

        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();
            _leftMouseBtnPressed = false;
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (_leftMouseBtnPressed)
            {
                _camera.Rotate(e.DeltaX, e.DeltaY);
            }
        }

        private void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            var viewMatrix = _camera.GetViewMatrix();
            GL.LoadMatrix(ref viewMatrix);

            plane.Draw();
        }
    }
}
