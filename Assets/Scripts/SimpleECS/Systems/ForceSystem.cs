using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class ForceSystem : System
    {

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Velocity), typeof(Force));

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                Transform p = entity.GetComponent<Transform>();
                Velocity v = entity.GetComponent<Velocity>();
                Force a = entity.GetComponent<Force>();

                Vector3 speed = v.Value;
                Vector3 localDirection = Vector3.up;
                Vector3 direction = p.TransformDirection(localDirection);
                Vector3 acceleration = direction * a.Value;
                Vector3 newSpeed = speed + acceleration * Time.fixedDeltaTime;
                v.Value = Vector3.ClampMagnitude(newSpeed, v.MaxValue);

            }
        }
    }
}