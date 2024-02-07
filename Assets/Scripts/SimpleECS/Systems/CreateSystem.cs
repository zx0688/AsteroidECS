using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class CreateSystem : System
    {

        private float asteroidCreationTimestamp;
        private float shipCreationTimestamp;


        public CreateSystem()
        {
            AddEntity(Creator.Create("Ship"));
            AddEntity(Creator.Create("UFO"));

            for (int i = 0; i < 5; i++)
                AddEntity(Creator.Create("Asteroid"));

            asteroidCreationTimestamp = Time.time + Random.Range(6, 9);
            shipCreationTimestamp = Time.time + Random.Range(6, 15);
        }

        override public void Update()
        {
            if (Time.time > asteroidCreationTimestamp)
            {
                AddEntity(Creator.Create("Asteroid"));
                asteroidCreationTimestamp = Time.time + Random.Range(6, 9);
            }

            if (Time.time > shipCreationTimestamp)
            {
                AddEntity(Creator.Create("UFO"));
                shipCreationTimestamp = Time.time + Random.Range(6, 15);
            }

        }

    }
}