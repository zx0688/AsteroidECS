using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;

namespace ECS
{
    public class FollowSystem : System
    {
        GameObject ship;
        Transform transform;
        Player player;

        public FollowSystem(GameData gameData) : base(gameData)
        {
        }

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(FollowPlayer));

        override protected void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();

            ship = GetEntities(typeof(Player)).FirstOrDefault();
            transform = ship.GetComponent<Transform>();
            player = ship.GetComponent<Player>();
        }

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                Transform p = entity.GetComponent<Transform>();
                Velocity v = entity.GetComponent<Velocity>();
                FollowPlayer fp = entity.GetComponent<FollowPlayer>();

                //if ship was crashed stop item
                Vector3 directionToTarget = gameData.Failed ? Vector3.zero : (transform.position - p.position).normalized;
                v.Value = directionToTarget * fp.Velocity;
            }
        }
    }
}