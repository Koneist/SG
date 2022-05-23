using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw5.Object
{
    internal struct BoxCollider
    {
        private Vector3 _frontRightTop;
        private Vector3 _frontLeftTop;
        private Vector3 _backLeftTop;
        private Vector3 _backRightTop;
        private Vector3 _frontRightBottom;
        private Vector3 _frontLeftBottom;
        private Vector3 _backLeftBottom;
        private Vector3 _backRightBottom;

        private Vector3 _position;

        private static List<Vector3> _baseVerteces = new()
        {
            new(-1, -1, -1),          // 0
            new(+1, -1, -1),          // 1
            new(+1, +1, -1),          // 2
            new(-1, +1, -1),          // 3
            new(-1, -1, +1),          // 4
            new(+1, -1, +1),          // 5
            new(+1, +1, +1),          // 6
            new(-1, +1, +1),          // 7
        };

        public BoxCollider(Vector3 position, float length, float width, float height)
        {
            var collider = new List<Vector3>(_baseVerteces.Count);
            foreach (var vertex in _baseVerteces)
            {
                var vec = vertex;
                vec.X *= length * 0.5f;
                vec.Y *= width * 0.5f;
                vec.Z *= height * 0.5f;
                collider.Add(vec);
            }
            _position = position;
            _frontRightTop = Vector3.Zero;
            _frontLeftTop = Vector3.Zero;
            _backLeftTop = Vector3.Zero;
            _backRightTop = Vector3.Zero;
            _frontRightBottom = Vector3.Zero;
            _frontLeftBottom = Vector3.Zero;
            _backLeftBottom = Vector3.Zero;
            _backRightBottom = Vector3.Zero;
            SetNewCoord(out _frontRightTop, collider[0]);
            SetNewCoord(out _frontLeftTop, collider[1]);
            SetNewCoord(out _backLeftTop, collider[2]);
            SetNewCoord(out _backRightTop, collider[3]);
            SetNewCoord(out _frontRightBottom, collider[4]);
            SetNewCoord(out _frontLeftBottom, collider[5]);
            SetNewCoord(out _backLeftBottom, collider[6]);
            SetNewCoord(out _backRightBottom, collider[7]);
        }

        public BoxCollider(Vector3 position,
                           Vector3 frontRightTop,
                           Vector3 frontLeftTop,
                           Vector3 backLeftTop,
                           Vector3 backRightTop,
                           Vector3 frontRightBottom,
                           Vector3 frontLeftBottom,
                           Vector3 backLeftBottom,
                           Vector3 backRightBottom)
        {
            _position = position;
            _frontRightTop = Vector3.Zero;
            _frontLeftTop = Vector3.Zero;
            _backLeftTop = Vector3.Zero;
            _backRightTop = Vector3.Zero;
            _frontRightBottom = Vector3.Zero;
            _frontLeftBottom = Vector3.Zero;
            _backLeftBottom = Vector3.Zero;
            _backRightBottom = Vector3.Zero;
            SetNewCoord(out _frontRightTop, frontRightTop);
            SetNewCoord(out _frontLeftTop, frontLeftTop);
            SetNewCoord(out _backLeftTop, backLeftTop);
            SetNewCoord(out _backRightTop, backRightTop);
            SetNewCoord(out _frontRightBottom, frontRightBottom);
            SetNewCoord(out _frontLeftBottom, frontLeftBottom);
            SetNewCoord(out _backLeftBottom, backLeftBottom);
            SetNewCoord(out _backRightBottom, backRightBottom);
            
        }

        public Vector3 FrontRightTop 
        {
            get => _frontRightTop;
            set => SetNewCoord(out _frontRightTop, value); 
        }
        public Vector3 FrontLeftTop 
        { 
            get => _frontLeftTop; 
            set => SetNewCoord(out _frontLeftTop, value); 
        }
        public Vector3 BackLeftTop
        {
            get => _backLeftTop;
            set => SetNewCoord(out _backLeftTop, value);
        }
        public Vector3 BackRightTop
        {
            get => _backRightTop;
            set => SetNewCoord(out _backRightTop, value);
        }
        public Vector3 FrontRightBottom
        {
            get => _frontRightBottom;
            set => SetNewCoord(out _frontRightBottom, value);
        }
        public Vector3 FrontLeftBottom
        {
            get => _frontLeftBottom;
            set => SetNewCoord(out _frontLeftBottom, value);
        }
        public Vector3 BackLeftBottom
        {
            get => _backLeftBottom;
            set => SetNewCoord(out _backLeftBottom, value);
        }
        public Vector3 BackRightBottom
        {
            get => _backRightBottom;
            set => SetNewCoord(out _backRightBottom, value);
        }

        public Vector3 Postition
        {
            get => _position;
            set 
            {
                _position = value;
                SetNewCoord(out _frontRightTop, _frontRightTop);
                SetNewCoord(out _frontLeftTop, _frontLeftTop);
                SetNewCoord(out _backLeftTop, _backLeftTop);
                SetNewCoord(out _backRightTop, _backRightTop);
                SetNewCoord(out _frontRightBottom, _frontRightBottom);
                SetNewCoord(out _frontLeftBottom, _frontLeftBottom);
                SetNewCoord(out _backLeftBottom, _backLeftBottom);
                SetNewCoord(out _backRightBottom, _backRightBottom);
            }
        }

        private void SetNewCoord(out Vector3 outCoord, Vector3 coord)
        {
            outCoord = new(new Vector4(coord) * Matrix4.CreateTranslation(_position));
        }

    }
}
