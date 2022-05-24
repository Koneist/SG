using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace lw6
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
                Title = "Lw6",
                WindowState = WindowState.Normal,
                WindowBorder = WindowBorder.Resizable,
                Size = new Vector2i(DEFAULT_WIDTH, DEFAULT_HEIGHT)
            };
            using (var window = Window.StartWindow(nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
