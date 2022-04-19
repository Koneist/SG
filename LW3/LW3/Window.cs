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
using LW3.Shapes;

namespace LW3
{
    internal class Window : GameWindow
    {
        private class PictureDraw
        {
            public List<BufferObject> _bufObjects;
            public Vector3 scale;
            public Vector3 translation;

            public PictureDraw(List<BufferObject> bufObjects, Vector3 scale, Vector3 translation)
            {
                _bufObjects = bufObjects;
                this.scale = scale;
                this.translation = translation;
            }
        }
        public static Window StartWindow(NativeWindowSettings nativeWindowSettings)
        {
            return new Window(GameWindowSettings.Default, nativeWindowSettings);
        }

        private List<BufferObject> _bufObjects;
        private List<PictureDraw> _pictureDraws;
        private float _baseWidth;
        private float _baseHeight;

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
            _pictureDraws = new();

            _baseWidth = nativeWindowSettings.Size.X;
            _baseHeight = nativeWindowSettings.Size.Y;
        }

        public void DrawPicture(Picture picture, Vector3 scale, Vector3 translation)
        {
            _pictureDraws.Add(new(new(), scale, translation));
            foreach(var shape in picture.Shapes)
            { 
                shape.Draw(this);
            }
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
            //прочесть про матрицы проецирования
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Projection);

            
            GL.Viewport(0, 0, e.Width, e.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();

            float scaleX = e.Width / _baseWidth;
            float scaleY = e.Height / _baseHeight;

            float scale = scaleY > scaleX ? scaleX : scaleY;

            GL.Scale(scale, scale, 1);

            GL.Ortho(-e.Width / 2, e.Width / 2, -e.Height / 2, e.Height / 2, -1, 1);
            
            GL.MatrixMode(MatrixMode.Modelview);
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
            
            foreach(var picture in _pictureDraws)
            {
                GL.PushMatrix();
                GL.Translate(picture.translation);
                GL.Scale(picture.scale);
                foreach (var bufObject in picture._bufObjects)
                {
                    bufObject.Draw();
                }
                GL.PopMatrix();
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

            CreateBufferObject(verteces, PrimitiveType.TriangleFan, fillColor);
            CreateBufferObject(verteces[2..], PrimitiveType.LineStrip, strokeColor, strokeWidth);
        }

        public void DrawPolygon(float[] points, Color4 fillColor, Color4 strokeColor, float strokeWidth)
        {
            CreateBufferObject(points, PrimitiveType.Polygon, fillColor);
            CreateBufferObject(points, PrimitiveType.LineLoop, strokeColor, strokeWidth);
        }

        public void DrawBrokenLine(float[] verteces, Color4 strokeColor, float strokeWidth)
        {
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

            _pictureDraws.Last()._bufObjects.Add(buffer);
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
