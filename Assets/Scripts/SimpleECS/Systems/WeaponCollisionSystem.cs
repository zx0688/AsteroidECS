using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;

namespace ECS
{
    public class WeaponCollisionSystem : CollisionSystem
    {
        private List<GameObject> weapons;
        private List<GameObject> targets;

        public WeaponCollisionSystem(GameData gameData) : base(gameData)
        {
        }

        override protected void OnEntitiesChanged()
        {
            targets = GetEntities(typeof(Target));
            weapons = GetEntities(typeof(Weapon));
        }

        override public void Update()
        {
            foreach (var weapon in weapons)
            {
                foreach (var target in targets)
                {
                    if (target != weapon && Hit(weapon.GetComponent<LineRenderer>(), target.GetComponent<LineRenderer>()))
                    {
                        if (target.TryGetComponent(out Asteroid a) && a.baseAsteroid)
                            Split(target);

                        target.GetComponent<HP>().Value = 0;

                        if (weapon.TryGetComponent(out HP hp))
                            hp.Value = 0;

                        gameData.Score += target.GetComponent<Target>().Score;


                    }
                }
            }

        }

        private void Split(GameObject oldAsteroid)
        {
            Transform t = oldAsteroid.GetComponent<Transform>();
            Velocity v = oldAsteroid.GetComponent<Velocity>();

            for (int count = 0; count <= (int)Random.Range(2f, 5f); count++)
            {
                GameObject newAsteroid = Creator.Create("Asteroid");
                var initialSpeed = Random.insideUnitSphere * 2 + v.Value;
                initialSpeed.z = 0;
                newAsteroid.GetComponent<Transform>().localScale = t.localScale * 0.5f;
                newAsteroid.GetComponent<Transform>().position = t.position;
                newAsteroid.GetComponent<Velocity>().Value = initialSpeed;
                newAsteroid.GetComponent<Velocity>().MaxValue = 10f;
                newAsteroid.GetComponent<Rotation>().Angle = 0f;
                newAsteroid.GetComponent<Asteroid>().baseAsteroid = false;

                AddEntity(newAsteroid);
            }

        }
    }
}