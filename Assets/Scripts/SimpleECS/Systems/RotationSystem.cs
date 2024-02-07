using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class RotationSystem : System
    {
        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Rotation));

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                Transform t = entity.GetComponent<Transform>();
                Rotation r = entity.GetComponent<Rotation>();

                var rot = t.rotation.eulerAngles;
                rot.z += r.Angle;
                t.rotation = Quaternion.Euler(rot);
            }
        }
    }
}