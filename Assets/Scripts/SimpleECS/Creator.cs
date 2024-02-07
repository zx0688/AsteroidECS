using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class Creator : MonoBehaviour
    {
        public static GameObject Create(string assetPath)
        {
            switch (assetPath)
            {
                case "Asteroid":
                    return Asteroid(ObjectPool.Get(assetPath));
                case "Ship":
                    return Ship(ObjectPool.Get(assetPath));
                case "UFO":
                    return UFO(ObjectPool.Get(assetPath));
                case "Bullet":
                    return Bullet(ObjectPool.Get(assetPath));
                case "Laser":
                    return Laser(ObjectPool.Get(assetPath));

                default: throw new Exception("undefind asset path");
            }
        }

        private static GameObject Laser(GameObject laser)
        {
            laser.GetComponent<Duration>().ActivateTime = Time.time;
            return laser;
        }


        private static GameObject Bullet(GameObject bullet)
        {
            bullet.GetComponent<HP>().Value = 1;
            return bullet;
        }

        private static GameObject UFO(GameObject ufo)
        {
            ufo.GetComponent<HP>().Value = 2;
            ufo.GetComponent<Transform>().position = RandomPoistion();
            ufo.GetComponent<Velocity>().MaxValue = 3f;
            return ufo;
        }

        private static GameObject Ship(GameObject player)
        {
            player.GetComponent<HP>().Value = 2;
            player.GetComponent<Transform>().position = Vector3.zero;
            player.GetComponent<Velocity>().Value = Vector3.zero;
            player.GetComponent<Velocity>().MaxValue = 3f;
            player.GetComponent<Force>().Value = 0;
            return player;
        }


        private static GameObject Asteroid(GameObject asteroid)
        {
            var initialSpeed = UnityEngine.Random.insideUnitSphere * 1;
            initialSpeed.z = 0;

            asteroid.GetComponent<Transform>().localScale = RandomSize();
            asteroid.GetComponent<Transform>().position = RandomPoistion();
            asteroid.GetComponent<Velocity>().Value = initialSpeed;
            asteroid.GetComponent<HP>().Value = 2;
            asteroid.GetComponent<Velocity>().MaxValue = 10f;
            asteroid.GetComponent<Rotation>().Angle = 1f;
            return asteroid;
        }

        private static Vector3 RandomSize()
        {
            float s = UnityEngine.Random.Range(0.3f, 1.1f);
            return new Vector3(s, s, s);
        }
        private static Vector3 RandomPoistion()
        {
            Vector3 randomPoint = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value);
            randomPoint.z = Camera.main.WorldToViewportPoint(Vector3.zero).z;
            var viewportToWorldPoint = Camera.main.ViewportToWorldPoint(randomPoint);
            viewportToWorldPoint.z = 0;
            return viewportToWorldPoint;
        }


    }
}
