using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;

namespace ECS
{
    public class CollisionSystem : System
    {
        private const float CIRCLE_RADIUS = 0.1f;

        public CollisionSystem(GameData gameData) : base(gameData)
        {
        }

        protected bool Hit(LineRenderer lineRenderer1, LineRenderer lineRenderer2)
        {
            Vector3[] points1 = new Vector3[lineRenderer1.positionCount];
            lineRenderer1.GetPositions(points1);

            Vector3[] points2 = new Vector3[lineRenderer2.positionCount];
            lineRenderer2.GetPositions(points2);

            Transform transform1 = lineRenderer1.transform;
            for (int i = 0; i < points1.Length; i++)
                points1[i] = transform1.TransformPoint(points1[i]);

            Transform transform2 = lineRenderer2.transform;
            for (int j = 0; j < points2.Length; j++)
                points2[j] = transform2.TransformPoint(points2[j]);

            // Check if any line segment intersects with circles centered at points of the second line renderer
            for (int i = 0; i < points1.Length - 1; i++)
                if (CircleLineIntersection(points2, points1[i], points1[i + 1], CIRCLE_RADIUS))
                    return true;

            if (CircleLineIntersection(points2, points1[points1.Length - 1], points1[0], CIRCLE_RADIUS))
                return true;

            for (int i = 0; i < points2.Length - 1; i++)
                if (CircleLineIntersection(points1, points2[i], points2[i + 1], CIRCLE_RADIUS))
                    return true;

            if (CircleLineIntersection(points1, points2[points2.Length - 1], points2[0], CIRCLE_RADIUS))
                return true;

            return false;
        }

        bool CircleLineIntersection(Vector3[] circleCenters, Vector3 start, Vector3 end, float circleRadius)
        {
            for (int i = 0; i < circleCenters.Length; i++)
            {
                Vector3 circleCenter = circleCenters[i];
                float closestPoint = Mathf.Clamp01(Vector3.Dot(circleCenter - start, end - start) / (end - start).sqrMagnitude);
                Vector3 projection = start + closestPoint * (end - start);
                float distance = Vector3.Distance(circleCenter, projection);

                if (distance < circleRadius)
                    return true;
            }

            return false;
        }

    }
}