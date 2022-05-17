using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw5
{
    internal class DirectLight
    {
        private Vector4 _direction;
        private Vector4 _diffuseColor = new(0.8f, 0.8f, 0.8f, 1.0f);
        private Vector4 _ambientColor = new(0.2f, 0.2f, 0.2f, 1.0f);
        private Vector4 _specularColor = new(0.5f, 0.5f, 0.5f, 1.0f);

        public void SetDirection(Vector3 direction) => _direction = new(direction, 1f);
        public void SetDiffuseIntensity(Vector4 color) => _diffuseColor = color;
        public void SetAmbientIntensity(Vector4 color) => _ambientColor = color;
        public void SetSpecularIntensity(Vector4 color) => _specularColor = color;

        public DirectLight(Vector3 direction)
        {
            _direction = new(direction, 1.0f);
        }

        public void Apply(LightName light)
        {
            GL.Light(light, LightParameter.Position, _direction);
            GL.Light(light, LightParameter.Diffuse, _diffuseColor);
            GL.Light(light, LightParameter.Ambient, _ambientColor);
            GL.Light(light, LightParameter.Specular, _specularColor);
        }
    }
}
