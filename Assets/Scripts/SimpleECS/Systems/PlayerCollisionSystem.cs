using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;

namespace ECS
{
    public class PlayerCollisionSystem : CollisionSystem
    {
        GameObject ship;
        LineRenderer renderer;
        Player player;

        public PlayerCollisionSystem(GameData gameData) : base(gameData)
        {
        }

        override protected bool Filter(GameObject entity) => HasComponents(entity, typeof(Target));

        override protected void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();

            ship = GetEntities(typeof(Player)).FirstOrDefault();
            renderer = ship.GetComponent<LineRenderer>();
            player = ship.GetComponent<Player>();
        }

        override public void Update()
        {
            foreach (var entity in entities)
            {
                if (!gameData.Failed && Hit(renderer, entity.GetComponent<LineRenderer>()))
                {
                    gameData.Failed = true;
                    ship.SetActive(false);
                }
            }

        }
    }
}