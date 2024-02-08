using System;
using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;


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
        ufo.GetComponent<Velocity>().MaxValue = 3f;
        return ufo;
    }

    private static GameObject Ship(GameObject ship)
    {
        ship.GetComponent<HP>().Value = 2;
        ship.GetComponent<Transform>().position = Vector3.zero;
        ship.GetComponent<Velocity>().Value = Vector3.zero;
        ship.GetComponent<Velocity>().MaxValue = 3f;
        ship.GetComponent<Force>().Value = 0;
        return ship;
    }


    private static GameObject Asteroid(GameObject asteroid)
    {
        var initialSpeed = UnityEngine.Random.insideUnitSphere * 1;
        initialSpeed.z = 0;

        asteroid.GetComponent<Transform>().localScale = RandomSize();
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


}

