using UnityEngine;

namespace Shared.Exstension
{
    public static class TransformExstentions
    {
        /// <summary>
        /// Check if the target is within the line of sight
        /// </summary>
        /// <param name="origin">Transform origin</param>
        /// <param name="target">Target direction</param>
        /// <param name="fieldOfView">Field of view</param>
        /// <param name="collisionMask">Check against layer</param>
        /// <param name="Offset">Transforms origin offset</param>
        /// <returns>yes or no</returns>
        public static bool IsInLineOfSight(this Transform origin, Vector3 target, float fieldOfView, LayerMask collisionMask, Vector3 Offset)
        {
            Vector3 direction = target - origin.position;

            if (Vector3.Angle(origin.forward, direction.normalized) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(origin.position, target);

                //Something is blocking the view..
                if (Physics.Raycast(origin.position + Offset + origin.forward * .3f, direction.normalized, distanceToTarget, collisionMask))
                    return false;

                return true;
            }
            return false;
        }
    }
}
