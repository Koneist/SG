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
using lw5.Object;

namespace lw5
{
    internal class Window : GameWindow
    {
        private Figure _figure = new Figure(1f);
        private Planet _planet = new(@"E:\Projects\repository\SG\lw5 - labyrinth\lw5\Texture\2k_earth_daymap.jpg");
        private Wall _wall = new(new(1, 1, 0), 1, 0.2f, 1, @"E:\Projects\repository\SG\lw5 - labyrinth\lw5\Texture\pexels-pixabay-220182.jpg");
        private Wall _wall1 = new(new(-1, 1, 0), 1, 0.2f, 1, @"E:\Projects\repository\SG\lw5 - labyrinth\lw5\Texture\pexels-pixabay-220182.jpg");
        // Размер стороны куба

        private const float Z_NEAR = 0.1f;
        private const float Z_FAR = 10;
        private bool _leftMouseBtnPressed = false;

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
            //GL.Enable(EnableCap.Lighting);
            //GL.Enable(EnableCap.Light2);
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.DepthTest);
            

            //_planet.SetInclinationAngle();
            //_planet.SetRotationSpeed(10);
            _planet.SetInclinationAngle(90);
            //DirectLight light = new(new(0, 0, 0));
            //light.SetDiffuseIntensity(new(0.5f, 0.5f, 0.5f, 1f));
            //light.SetAmbientIntensity(new(0.3f, 0.3f, 0.3f, 1.0f));
            //light.SetSpecularIntensity(new(1.0f, 1.0f, 1.0f, 1.0f));
            //light.Apply(LightName.Light2);


            _camera = new Camera(Vector3.UnitZ * 2, (float)Size.X / (float)Size.Y);
            base.OnLoad();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!IsFocused)
                return;
            
            var input = KeyboardState;



            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (input.IsKeyDown(Keys.W))
            {
                _camera.MoveForward((float)e.Time);
                //_camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.MoveBack((float)e.Time);
                //_camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.MoveLeft((float)e.Time);
                //_camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.MoveRight((float)e.Time);
                //.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.MoveUp((float)e.Time);
                //_camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.MoveDown((float)e.Time);
                //_camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
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
            Draw();

            SwapBuffers();
            _planet.Animate((float)args.Time);

            //if(time > 0.5f)
            //{
            //_camera.Yaw = (float)args.Time;

            //}
            //    time += (float)args.Time;
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
            const float sensitivity = 0.2f;
            if (_leftMouseBtnPressed)
            {
                //_camera.Yaw += e.DeltaX * sensitivity;
                //_camera.Pitch += e.DeltaY * sensitivity;
                _camera.Rotate(e.DeltaX, e.DeltaY);
            }
        }

        private void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            var viewMatrix = _camera.GetViewMatrix();
            GL.LoadMatrix(ref viewMatrix);

            //_planet.Draw();
            _wall1.Draw();
            _wall.Draw();
            //_figure.Draw();
        }
    }
}
