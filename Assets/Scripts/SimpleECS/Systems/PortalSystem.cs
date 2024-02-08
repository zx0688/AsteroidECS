using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class PortalSystem : System
    {
        public PortalSystem(GameData gameData) : base(gameData)
        {
        }

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Portal));

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                Transform p = entity.GetComponent<Transform>();
                Vector3 viewportPoint = Camera.main.WorldToViewportPoint(p.position);
                if (viewportPoint.x < 0)
                    viewportPoint.x = 1;
                else if (viewportPoint.x > 1)
                    viewportPoint.x = 0;
                else if (viewportPoint.y < 0)
                    viewportPoint.y = 1;
                else if (viewportPoint.y > 1)
                    viewportPoint.y = 0;

                p.position = Camera.main.ViewportToWorldPoint(viewportPoint);


            }
        }
    }
}