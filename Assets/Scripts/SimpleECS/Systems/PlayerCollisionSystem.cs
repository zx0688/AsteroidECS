using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;

namespace ECS
{
    public class PlayerCollisionSystem : CollisionSystem
    {
        GameObject player;
        LineRenderer renderer;

        override protected bool Filter(GameObject entity) => HasComponents(entity, typeof(Target));

        override protected void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();

            player = GetEntities(typeof(Player))[0];

            renderer = player != null ? player.GetComponent<LineRenderer>() : null;
        }

        override public void Update()
        {
            foreach (var entity in entities)
            {
                if (renderer != null && Hit(renderer, entity.GetComponent<LineRenderer>()))
                {
                    player.GetComponent<HP>().Value = 0;

                }
            }

        }
    }
}