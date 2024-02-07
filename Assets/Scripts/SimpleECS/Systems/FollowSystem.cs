using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class FollowSystem : System
    {
        GameObject player;
        Transform transformPlayer;

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(FollowPlayer));

        override protected void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();

            player = GetEntities(typeof(Player))[0];
            transformPlayer = player.GetComponent<Transform>();
        }

        override public void Update()
        {
            foreach (GameObject entity in entities)
            {
                Transform p = entity.GetComponent<Transform>();
                Velocity v = entity.GetComponent<Velocity>();
                FollowPlayer fp = entity.GetComponent<FollowPlayer>();

                Vector3 directionToTarget = (transformPlayer.position - p.position).normalized;
                v.Value = directionToTarget * fp.Velocity;
            }
        }
    }
}