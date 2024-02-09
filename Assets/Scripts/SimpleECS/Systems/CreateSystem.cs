using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class CreateSystem : CollisionSystem
    {
        Player player;
        LineRenderer lineRenderer;

        private float asteroidCreationTimestamp;
        private float shipCreationTimestamp;

        public CreateSystem(GameData gameData) : base(gameData)
        {
            Reset();
        }

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Player));

        protected override void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();

            if (entities.Count > 0)
            {
                player = entities[0].GetComponent<Player>();
                lineRenderer = player.gameObject.GetComponent<LineRenderer>();
            }
        }

        private void Reset()
        {
            ClearScene();

            AddEntity(Creator.Create("Ship"));

            for (int i = 0; i < 5; i++)
                AddEntity(FixPosition(Creator.Create("Asteroid")));

            AddEntity(FixPosition(Creator.Create("UFO")));

            gameData.Restart = false;
            gameData.Failed = false;
            gameData.Score = 0;

            asteroidCreationTimestamp = Time.time + Random.Range(6, 9);
            shipCreationTimestamp = Time.time + Random.Range(6, 15);
        }

        override public void Update()
        {
            if (player == null)
                return;

            if (!gameData.Failed && Time.time > asteroidCreationTimestamp)
            {
                AddEntity(FixPosition(Creator.Create("Asteroid")));
                asteroidCreationTimestamp = Time.time + Random.Range(6, 9);
            }

            if (!gameData.Failed && Time.time > shipCreationTimestamp)
            {
                AddEntity(FixPosition(Creator.Create("UFO")));
                shipCreationTimestamp = Time.time + Random.Range(6, 15);
            }

            if (gameData.Restart)
            {
                Reset();
            }
        }

        private GameObject FixPosition(GameObject entity)
        {
            LineRenderer e = entity.GetComponent<LineRenderer>();
            int iter = 0;
            do
            {
                entity.GetComponent<Transform>().position = RandomPosition();
                iter++;
            }
            while (Hit(e, lineRenderer) && iter < 200);

            return entity;
        }

        private Vector3 RandomPosition()
        {
            float radius = 2 + Random.value * 4;
            Vector3 center = player.GetComponent<Transform>().position;

            float angle = UnityEngine.Random.value * Mathf.PI * 2;
            Vector3 randomOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), center.z) * radius;
            Vector3 randomPosition = center + randomOffset;

            return randomPosition;

        }

    }
}