using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;

namespace ECS
{
    public class CollisionSystem : System
    {
        public CollisionSystem(GameData gameData) : base(gameData)
        {
        }

        protected bool Hit(LineRenderer lineRenderer1, LineRenderer lineRenderer2)
        {
            Transform transform1 = lineRenderer1.transform;
            if (lineRenderer2.bounds.Contains(transform1.position))
                return true;

            Vector3[] points1 = new Vector3[lineRenderer1.positionCount];
            lineRenderer1.GetPositions(points1);

            Vector3[] points2 = new Vector3[lineRenderer2.positionCount];
            lineRenderer2.GetPositions(points2);


            for (int i = 0; i < points1.Length; i++)
                points1[i] = transform1.TransformPoint(points1[i]);

            Transform transform2 = lineRenderer2.transform;
            for (int j = 0; j < points2.Length; j++)
                points2[j] = transform2.TransformPoint(points2[j]);

            for (int i = 0; i < points1.Length - 1; i++)
                for (int j = 0; j < points2.Length; j++)
                {
                    if (j == points2.Length - 1)
                    {
                        if (LinesIntersect(points1[i], points1[i + 1], points2[j], points2[0]))
                            return true;
                    }
                    else if (LinesIntersect(points1[i], points1[i + 1], points2[j], points2[j + 1]))
                        return true;
                }
            for (int j = 0; j < points2.Length; j++)
            {
                if (j == points2.Length - 1)
                {
                    if (LinesIntersect(points1[points1.Length - 1], points1[0], points2[j], points2[0]))
                        return true;
                }
                else if (LinesIntersect(points1[points1.Length - 1], points1[0], points2[j], points2[j + 1]))
                    return true;
            }

            return false;
        }

        bool LinesIntersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            float denominator = (b.x - a.x) * (d.y - c.y) - (b.y - a.y) * (d.x - c.x);

            if (denominator == 0)
                return false;

            float t = ((c.x - a.x) * (d.y - c.y) - (c.y - a.y) * (d.x - c.x)) / denominator;
            float u = -((a.x - b.x) * (c.y - a.y) - (a.y - b.y) * (c.x - a.x)) / denominator;

            return t >= 0 && t <= 1 && u >= 0 && u <= 1;
        }

    }
}