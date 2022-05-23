using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw5.Object
{
    internal static class CollisionManager
    {
        private static List<GameObject> gameObjects = new();
        
        public static void AddCollider(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public static void RemoveCollider(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }

        public static Vector3 GetCollision(GameObject gameObject)
        {
            var MoveVector = Vector3.Zero;
            foreach(var collider in gameObjects)
            {
                if (gameObject == collider)
                    continue;

                if (!Intersect(gameObject, collider))
                    continue;

            }

            

            return MoveVector;
        }

        private static bool Intersect(GameObject a, GameObject b)
        {
            var colliderA = a.BoxCollider;
            var colliderB = b.BoxCollider;
            return (colliderA.BackLeftBottom.X <= colliderB.BackRightBottom.X && colliderA.BackRightBottom.X >= colliderB.BackLeftBottom.X) &&
                   (colliderA.BackRightBottom.Y <= colliderB.BackRightTop.Y && colliderA.BackRightTop.Y >= colliderB.BackRightBottom.Y) &&
                   (colliderA.BackRightBottom.Z <= colliderB.FrontRightBottom.Z && colliderA.FrontRightBottom.Z >= colliderB.BackRightBottom.Z);
        }
    }
}
