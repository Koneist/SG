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

            SwapBuffers();
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        public void DrawEllipse(Vector2 center, float rx, float ry, Color4 fillColor, Color4 strokeColor)
        {
            const float points = 24;
            const float step = 2 * MathF.PI / points;
            
            float[] verteces = new float[(int)(2 * (points + 1))];

            verteces[0] = center.X;
            verteces[1] = center.Y;

            for (int point = 0, j = 2; point < points; point++, j += 2)
            {
                verteces[j] = center.X + rx * MathF.Sin(step * point);
                verteces[j + 1] = center.Y + ry * MathF.Cos(step * point);
            }

            for(int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 2; j++)
                {

                    Console.Write($"{verteces[i]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
