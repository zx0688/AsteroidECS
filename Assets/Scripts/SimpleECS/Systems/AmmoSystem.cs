using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class AmmoSystem : System
    {
        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Ammo));

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                Ammo a = entity.GetComponent<Ammo>();

                if (a.IsFinished(Time.time) && a.Count < a.MaxCount)
                {
                    a.Count++;
                    if (a.Count != a.MaxCount)
                        a.StartReloadTimestamp = Time.time;

                }

            }
        }
    }
}