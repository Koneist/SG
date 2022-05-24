using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw6.Object
{
    internal class Camera : GameObject
    {
        private Vector3 _front = -Vector3.UnitZ;

        private Vector3 _up = Vector3.UnitY;

        private Vector3 _right = Vector3.UnitX;

        private float _pitch;

        private float _yaw = -MathHelper.PiOver2;

        private float _fov = MathHelper.PiOver2;

        private float _speed = 1.5f;

        private float _sensitivity = 0.2f;

        public Camera(Vector3 position, float aspectRatio)
            : base(position, 0.2f, 0.2f, 0.2f)
        {
            //Position = position;
            AspectRatio = aspectRatio;
        }

        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Vector3 Right => _right;

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        private void UpdateVectors()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }

        public void MoveForward(float frameTime)
        {
            var front = Front;
            front.Y = 0;
            front.Normalize();
            Move(front * _speed * frameTime);
            //Position += front * _speed * frameTime;
        }

        public void MoveBack(float frameTime)
        {
            var front = Front;
            front.Y = 0;
            front.Normalize();
            Move(front * -_speed * frameTime);
            //Position -= front * _speed * frameTime;
        }

        public void MoveLeft(float frameTime)
        {
            Move(Right * -_speed * frameTime);
            //Position -= Right * _speed * frameTime;
        }

        public void MoveRight(float frameTime)
        {
            Move(Right * _speed * frameTime);
            //Position += Right * _speed * frameTime;
        }
        public void MoveUp(float frameTime)
        {
            var up = Vector3.UnitY;
            Move(up * _speed * frameTime);

            //Position += Up * _speed * frameTime;
        }
        public void MoveDown(float frameTime)
        {
            var up = Vector3.UnitY;
            Move(up * -_speed * frameTime);
            //Position -= Up * _speed * frameTime;
        }

        public void Rotate(float deltaX, float deltaY)
        {
            Yaw += deltaX * _sensitivity;
            Pitch += deltaY * _sensitivity;
        }
    }
}
