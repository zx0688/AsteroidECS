using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class MovementSystem : System
    {
        public MovementSystem(GameData gameData) : base(gameData)
        {
        }

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Velocity));

        override public void Update()
        {
            if (gameData.DisableMovement)
                return;

            foreach (GameObject entity in entities)
            {
                Transform p = entity.GetComponent<Transform>();
                Velocity v = entity.GetComponent<Velocity>();

                var pos = p.position;
                pos += v.Value * Time.fixedDeltaTime;
                p.position = pos;
            }
        }
    }
}