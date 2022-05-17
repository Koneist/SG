using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using lw5;

namespace lw5.Object
{
    internal class Planet
    {
        private Sphere _sphere;
        private float _rotationAngle;
        private float _inclinationAngle;
        private float _rotationSpeed;
        private Texture texture;
        private string _textureName;
        private bool isLoad = false;

        public Planet(string textureName, float radius = 1, int slices = 50, int stacks = 25)
        {
            _sphere = new(radius, slices, stacks);
            _rotationAngle = 0;
            _inclinationAngle = 0;
            _rotationSpeed = 0;
            _textureName = textureName;
        }

        public void Animate(float timeDelta)
        {
            _rotationAngle = _rotationAngle + _rotationSpeed * timeDelta % 360;
        }

        public void SetInclinationAngle(float inclinationAngle)
        {
            _inclinationAngle = inclinationAngle;
        }

        public void SetRotationSpeed(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
        }

        public void Draw()
        {
            if(!isLoad)
            {
                texture = Texture.LoadFromFile(_textureName);
                isLoad = true;
            }
            GL.Enable(EnableCap.Texture2D);
            texture.Use(TextureUnit.Texture0);

            GL.Rotate(_inclinationAngle, 0, 0, 1);
            GL.Rotate(_rotationAngle, 1, 0, 0);
            _sphere.Draw();
            GL.PopMatrix();
        }
    }
}
