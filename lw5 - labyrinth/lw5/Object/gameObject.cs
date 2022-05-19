using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw5.Object
{
    internal class gameObject
    {
        private Vector3 _position;

        public gameObject(Vector3 position)
        {
            _position = position;
        }

        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }

        public void Move(Vector3 moveVector)
        {

        }
    }
}
