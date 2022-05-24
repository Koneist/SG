using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw6.Object
{
    internal struct BoxCollider
    {
        private Box3 box;

        private static Vector3[] _compass =
        {
            Vector3.UnitZ,
            -Vector3.UnitZ,
            Vector3.UnitX,
            -Vector3.UnitX,
            Vector3.UnitY,
            -Vector3.UnitY
        };

        public BoxCollider(Vector3 position, float length, float width, float height)
        {
            var min = new Vector3(position.X - length * 0.5f, position.Y - height * 0.5f, position.Z - width * 0.5f);
            var max = new Vector3(position.X + length * 0.5f, position.Y + height * 0.5f, position.Z + width * 0.5f);
            box = new Box3(min, max);
        }



        public Vector3 FrontNormal => _compass[0];
        public Vector3 BackNormal => _compass[1];
        public Vector3 RightNormal => _compass[2];
        public Vector3 LeftNormal => _compass[3];
        public Vector3 UpNormal => _compass[4];
        public Vector3 DownNormal => _compass[5];

        public bool Contains(Box3 other) => box.Contains(other);
        public bool Contains(BoxCollider other) => box.Contains(other.box);
        public void Translate(Vector3 distance) => box.Translate(distance);

        public Vector3 GetExtructionVec(Vector3 moveVector)
        {
            var result = Vector3.Zero;
            var normMoveVector = moveVector.Normalized();

            float max = 0.0f;
            int bestMatch = 0;

            for(int i = 0; i< _compass.Length; ++i)
            {
                var dotProduct = Vector3.Dot(normMoveVector, _compass[i]);
                if(dotProduct > max)
                {
                    max = dotProduct;
                    bestMatch = i;
                }
            }

            return -_compass[bestMatch];
        }

        public Vector3 GetExtruction(Box3 other, Vector3 moveVector)
        {
            var result = Vector3.Zero;

            var minX = MathF.Max(box.Min.X, other.Min.X);
            var minY = MathF.Max(box.Min.Y, other.Min.Y);
            var minZ = MathF.Max(box.Min.Z, other.Min.Z);

            var maxX = MathF.Min(box.Max.X, other.Max.X);
            var maxY = MathF.Min(box.Max.Y, other.Max.Y);
            var maxZ = MathF.Min(box.Max.Z, other.Max.Z);

            var distance = new Vector3( maxX - minX, maxY - minY, maxZ - minZ);
            var min = distance.X > distance.Y ? distance.Y : distance.X;
            min = min > distance.Z ? distance.Z : min;

            if (distance.X == min) result.X = min;
            if (distance.Y == min) result.Y = min;
            if (distance.Z == min) result.Z = min;

            var direction = Vector3.One;
            moveVector.Normalize();

            if (box.Center.X > other.Center.X) direction.X *= -1;
            if (box.Center.Y > other.Center.Y) direction.Y *= -1;
            if (box.Center.Z > other.Center.Z) direction.Z *= -1;

            return result * direction;
        }
        public Vector3 GetExtruction(BoxCollider other, Vector3 moveVector) => GetExtruction(other.box, moveVector);

    }
}
