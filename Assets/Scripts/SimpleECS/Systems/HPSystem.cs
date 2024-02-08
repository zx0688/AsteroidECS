using System.Collections;
using System.Collections.Generic;
using ECS;
using Unity.VisualScripting;
using UnityEngine;

namespace ECS
{
    public class HPSystem : System
    {
        public HPSystem(GameData gameData) : base(gameData)
        {
        }

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