using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw6.Object
{
    internal class GameObject
    {
        private Vector3 _position;
        private bool _isColliding;

        public BoxCollider BoxCollider;

        public GameObject(Vector3 position, float length, float width, float height, bool isColliding = true)
        {
            _position = position;
            _isColliding = isColliding;
            CollisionManager.AddCollider(this);

            BoxCollider = new BoxCollider(position, length, width, height);
        }

        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }

        public bool IsColliding
        {
            get => _isColliding;
            set => _isColliding = value;
        }

        public void Move(Vector3 moveVector)
        {
            _position += moveVector;
            BoxCollider.Translate(moveVector);
            if (_isColliding)
            {
                var extruction = CollisionManager.GetExtruction(this, moveVector);
                _position += extruction;
                BoxCollider.Translate(extruction);
            }
        }

        
        
    }
}
