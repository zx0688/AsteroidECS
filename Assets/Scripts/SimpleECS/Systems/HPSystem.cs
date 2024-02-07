using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class HPSystem : System
    {
        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(HP));

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                HP h = entity.GetComponent<HP>();
                if (h.Value > 0)
                    continue;

                RemoveEntity(entity);
            }
        }
    }
}