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

        private List<BufferObject> _bufObjects;

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
            _bufObjects = new();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            foreach(var obj in _bufObjects)
                obj.Dispose();
            base.OnUnload();
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

            GL.Viewport(0, 0, e.Width, e.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();

            GL.Ortho(-e.Width / 2, e.Width / 2, -e.Height / 2, e.Height / 2, -1, 1);

            DrawBuffer();

            

            SwapBuffers();
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            DrawBuffer();
            
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        private void DrawBuffer()
        {
            
            foreach (var bufObject in _bufObjects)
            {
                bufObject.Draw();
            }

        }

        public void DrawEllipse(float centerX, float centerY, float rx, float ry, Color4 fillColor, Color4 strokeColor, float strokeWidth)
        {
            const float points = 50;
            const float step = 2 * MathF.PI / points;
            
            float[] verteces = new float[(int)(2 * (points + 2))];

            verteces[0] = centerX;
            verteces[1] = centerY;

            for (int point = 0, j = 2; point <= points; point++, j += 2)
            {
                verteces[j] = centerX + rx * MathF.Sin(step * point);
                verteces[j + 1] = centerY + ry * MathF.Cos(step * point);
            }

            //BufferObject FillCircle = new(BufferType.ArrayBuffer);
            //FillCircle.SetData(verteces, PrimitiveType.TriangleFan, fillColor);
            //BufferObject strokeObject = new(BufferType.ArrayBuffer);
            //strokeObject.SetData(verteces[2..], PrimitiveType.LineStrip, strokeColor, strokeWidth);

            //_bufObjects.Add(FillCircle);
            //_bufObjects.Add(strokeObject);
            CreateBufferObject(verteces, PrimitiveType.TriangleFan, fillColor);
            CreateBufferObject(verteces[2..], PrimitiveType.LineStrip, strokeColor, strokeWidth);
        }

        public void DrawPolygon(float[] points, Color4 fillColor, Color4 strokeColor, float strokeWidth)
        {
            //BufferObject fillPolygon = new(BufferType.ArrayBuffer);
            //fillPolygon.SetData(points, PrimitiveType.Polygon, fillColor);
            //BufferObject strokePolygon = new(BufferType.ArrayBuffer);
            //strokePolygon.SetData(points, PrimitiveType.LineLoop, strokeColor, strokeWidth);

            //_bufObjects.Add(fillPolygon);
            //_bufObjects.Add(strokePolygon);
            CreateBufferObject(points, PrimitiveType.Polygon, fillColor);
            CreateBufferObject(points, PrimitiveType.LineLoop, strokeColor, strokeWidth);
        }

        public void DrawBrokenLine(float[] verteces, Color4 strokeColor, float strokeWidth)
        {
            //BufferObject strokePolygon = new(BufferType.ArrayBuffer);
            //strokePolygon.SetData(points, PrimitiveType.LineStrip, strokeColor, strokeWidth);
            //_bufObjects.Add(strokePolygon);
            CreateBufferObject(verteces, PrimitiveType.LineStrip, strokeColor, strokeWidth);
        }


        public void DrawCurve(float[] points, Color4 fillColor, Color4 strokeColor, float strokeWidth)
        {
            var vectorPoints = PointsBufferToVertex(points);
            float t = 0.0f; float vertexFrequency = 0.00625f;
            List<float> newVerteces = new();
            while (MathF.Round(t, 2) <= 1.00f)
            {
                int pointCount = vectorPoints.Count;
                List<Vector2> temp = vectorPoints.ToList();
                while (pointCount > 1)
                {
                    for (int i = 0, j = 1; j < pointCount; i++, j++)
                        temp[i] = Vector2.Lerp(temp[i], temp[j], t);
                    pointCount--;
                }

                newVerteces.Add(temp[0].X);
                newVerteces.Add(temp[0].Y);

                t += vertexFrequency;
            }

            DrawPolygon(newVerteces.ToArray(), fillColor, strokeColor, strokeWidth);
        }
        
        private void CreateBufferObject(float[] verteces, PrimitiveType primitiveType, Color4 color, float strokeWidth = 1)
        {
            if (verteces.Length == 0 || color.A == Color4.Transparent.A)
                return;

            BufferObject buffer = new(BufferType.ArrayBuffer);
            buffer.SetData(verteces, primitiveType, color, strokeWidth);

            _bufObjects.Add(buffer);
        }

        private List<Vector2> PointsBufferToVertex(float[] verteces)
        {
            var newBuffer = new List<Vector2>(verteces.Length / 2);
            for(int i = 0; i < verteces.Length; i += 2)
            {
                newBuffer.Add(new(verteces[i], verteces[i + 1]));
            }
            return newBuffer;
        }
    }
}
