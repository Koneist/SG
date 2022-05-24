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
using System.IO;

namespace lw5
{
    internal class Window : GameWindow
    {
        private Wall _wall;
        private Wall _wall1;
        private Wall[] walls;
        Random random = new Random();
        // Размер стороны куба

        private const float Z_NEAR = 0.1f;
        private const float Z_FAR = 10;
        private bool _leftMouseBtnPressed = false;
        private Texture[] _textures;

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
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.DepthTest);

            var startPos = new Vector3(0, 0.5f, 0);
            _camera = new Camera(startPos, (float)Size.X / (float)Size.Y);

            var directory = Environment.CurrentDirectory;
            directory = Directory.GetParent(directory).Parent.Parent.FullName;
            directory += @"\Texture";
            var files = Directory.GetFiles(directory);
            _textures = new Texture[files.Length];
            for(int i = 0; i < files.Length; ++i)
            {
                _textures[i] = Texture.LoadFromFile(files[i]);
            }

            walls = new Wall[]
            {
                new(Vector3.Zero, 10, 10, 0.1f, _textures[0]),
                new(new(0, 0.55f, 4.9f), 0.2f, 9.6f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(4.9f, 0.55f, 0), 9.6f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(0, 0.55f, -4.9f), 0.2f, 9.6f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-4.9f, 0.55f, 0), 9.6f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),

                new(new(1.3f, 0.55f, 0.9f), 0.2f, 1.9f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-0.6f, 0.55f, 0.9f), 0.2f, 0.5f, 1, _textures[random.Next(1, _textures.Length - 1)]),

                new(new(0.6f, 0.55f, -0.9f), 0.2f, 0.5f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-0.575f, 0.55f, -0.9f), 0.2f, 0.45f, 1, _textures[random.Next(1, _textures.Length - 1)]),

                new(new(0.9f, 0.55f, 0.6f), 0.5f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(0.9f, 0.55f, -1.35f), 2f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),

                new(new(-0.9f, 0.55f, 2.2f), 3.7f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-0.9f, 0.55f, -0.675f), 0.65f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),

                new(new(-2.5f, 0.55f, 0.45f), 0.2f, 3f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-2.5f, 0.55f, -0.45f), 0.2f, 3f, 1, _textures[random.Next(1, _textures.Length - 1)]),

                new(new(-3.9f, 0.55f, 2.3f), 3.5f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-2f, 0.55f, 3.95f), 0.2f, 2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-2.9f, 0.55f, 2.6f), 2.5f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-2.3f, 0.55f, 1.45f), 0.2f, 1.0f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(0.3f, 0.55f, 1.8f), 0.2f, 2.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(2.35f, 0.55f, 1.35f), 1.1f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(0.2f, 0.55f, 3.4f), 1.3f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(1.3f, 0.55f, 4.3f), 1f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(1.4f, 0.55f, 2.85f), 0.2f, 2.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(2.4f, 0.55f, 3.9f), 0.2f, 2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(3.3f, 0.55f, 3.3f), 1f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(4f, 0.55f, 1.75f), 0.2f, 1.6f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(3.3f, 0.55f, 0.7f), 1.9f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(2.5f, 0.55f, -0.15f), 0.2f, 1.4f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(2.75f, 0.55f, -1.15f), 0.2f, 1.9f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(3.6f, 0.55f, -2.0f), 1.5f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(3.4f, 0.55f, -3.75f), 0.2f, 2.8f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(1.9f, 0.55f, -2.55f), 2.6f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-0.4f, 0.55f, -2.45f), 0.2f, 2.8f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-1.9f, 0.55f, -1.95f), 1.2f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-0.5f, 0.55f, -3.75f), 0.2f, 3f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-0.5f, 0.55f, -4.325f), 0.95f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-3f, 0.55f, -3.075f), 3.45f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-3.9f, 0.55f, -2.3f), 3.5f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
                new(new(-1.9f, 0.55f, 2.3f), 1.55f, 0.2f, 1, _textures[random.Next(1, _textures.Length - 1)]),
            };


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
                _camera.MoveBack((float)e.Time);
            if (input.IsKeyDown(Keys.A))
                _camera.MoveLeft((float)e.Time);
            if (input.IsKeyDown(Keys.D))
                _camera.MoveRight((float)e.Time);
            //if (input.IsKeyDown(Keys.Space))
            //    _camera.MoveUp((float)e.Time);
            //if (input.IsKeyDown(Keys.LeftShift))
            //    _camera.MoveDown((float)e.Time);

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
                _camera.Rotate(e.DeltaX, e.DeltaY);
            }
        }

        private void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            var viewMatrix = _camera.GetViewMatrix();
            GL.LoadMatrix(ref viewMatrix);

            foreach(var wall in walls)
                wall.Draw();
        }
    }
}
