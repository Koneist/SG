using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw4
{
    internal class Window : GameWindow
    {
        private const double FIELD_OF_VIEW = 60 * Math.PI / 180.0;
        // Размер стороны куба
        private const double CUBE_SIZE = 1;

        private const double Z_NEAR = 0.1;
        private const double Z_FAR = 10;
        public Window(GameWindowSettings gameWindowSettings,
                      NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {

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
            

            SwapBuffers();
            base.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            

            SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
