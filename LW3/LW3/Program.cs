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
        private const int DEFAULT_WIDTH = 1000;
        private const int DEFAULT_HEIGHT = 1000;
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
                //window.SetTransform();
                window.DrawPicture(Picture.Kopatych, new Vector3(0.5f,0.5f,1), new Vector3(300, -300, 1));
                window.DrawPicture(Picture.Kopatych, new Vector3(1.5f, 1, 1), new Vector3(-100, 200, 1));
                //Picture.Kopatych.Draw(window);
                //window.FuckGoBack();
                window.Run();
            }
        }
    }
}
