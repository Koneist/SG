using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace LW3
{
    internal class Program
    {
        private const int MAJOR_VER = 4;
        private const int MINOR_VER = 6;
        private const int DEFAULT_WIDTH = 1280;
        private const int DEFAULT_HEIGHT = 1280;
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Profile = ContextProfile.Compatability,
                APIVersion = new Version(MAJOR_VER, MINOR_VER),
                Title = "Lw3",
                WindowState = WindowState.Normal,
                WindowBorder = WindowBorder.Resizable,
                Size = new Vector2i(DEFAULT_WIDTH, DEFAULT_HEIGHT)
            };

            using (var window = Window.StartWindow(nativeWindowSettings))
            {
                window.DrawEllipse(new Vector2(0, 0), 200f, 200f, Color4.Red, Color4.Gray, 5f);
                window.DrawEllipse(new Vector2(0, 100), 200f, 150f, Color4.Blue, Color4.Transparent, 1);
                window.DrawPolygon( new float[]
                { 
                    100f, 100f,
                    200f, 100f,
                    200f, 200f,
                    //100f, 200f,
                }, Color4.Beige, Color4.Black, 5f);
                window.DrawBrokenLine(new float[]
                {
                    0, 0, 
                    100, 50,
                    -30, -70,
                }, Color4.Black, 10f);
                window.Run();
            }
        }
    }
}
