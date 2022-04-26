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
        private bool _leftMouseBtnPressed = false;

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
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light2);
            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.DepthTest);

            DirectLight light = new(new(0, 0, 0));
            light.SetDiffuseIntensity(new(0.5f, 0.5f, 0.5f, 1f));
            light.SetAmbientIntensity(new(0.3f, 0.3f, 0.3f, 1.0f));
            light.SetSpecularIntensity(new(1.0f, 1.0f, 1.0f, 1.0f));
            light.Apply(LightName.Light2);

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
                float xAngle = e.DeltaY * MathF.PI / Size.Y;
                float yAngle = e.DeltaX * MathF.PI / Size.X;
                RotateCamera(xAngle, yAngle);
            }
        }

        private void RotateCamera(float xAngle, float yAngle)
        {
            Vector3 xAxis = new(_cameraMatrix.M11, _cameraMatrix.M21, _cameraMatrix.M31);
            Vector3 yAxis = new(_cameraMatrix.M12, _cameraMatrix.M22, _cameraMatrix.M32);

            //_cameraMatrix *= Matrix4.CreateFromAxisAngle(xAxis, xAngle);
            //_cameraMatrix *= Matrix4.CreateFromAxisAngle(yAxis, yAngle);
            //GL.PushMatrix();
            //GL.LoadIdentity();
            //GL.LoadMatrix(ref _cameraMatrix);
            //GL.Rotate(xAngle, xAxis);
            //GL.PopMatrix();
            _figure.Rotate(xAngle, xAxis);
            _figure.Rotate(yAngle, yAxis);
            //_cameraMatrix = Orthonormalize(_cameraMatrix);
        }

        private void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref _cameraMatrix);

            _figure.Draw();
        }
    }
}
