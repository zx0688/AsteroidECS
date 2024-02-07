using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class DurationSystem : System
    {
        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Duration));

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                Duration d = entity.GetComponent<Duration>();

                if (!d.IsFinished(Time.time))
                    continue;

                RemoveEntity(entity);
            }
        }
    }
}