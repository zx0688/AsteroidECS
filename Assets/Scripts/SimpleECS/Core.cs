using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{

    public class Core : MonoBehaviour
    {
        [SerializeField] private UI ui;

        private Dictionary<Cycle, List<System>> systems = new Dictionary<Cycle, List<System>>();

        private enum Cycle
        {
            Update,
            FixedUpdate
        }

        void Start()
        {
            systems.Add(Cycle.Update, new List<System>());
            systems.Add(Cycle.FixedUpdate, new List<System>());

            GameData gameData = new GameData();

            Add(new CreateSystem(gameData), Cycle.Update);
            Add(new InputSystem(gameData), Cycle.Update);
            Add(new PortalSystem(gameData), Cycle.Update);
            Add(new HPSystem(gameData), Cycle.Update);
            Add(new FollowSystem(gameData), Cycle.Update);
            Add(new DurationSystem(gameData), Cycle.Update);
            Add(new AmmoSystem(gameData), Cycle.Update);
            Add(new UISystem(ui, gameData), Cycle.Update);

            Add(new PlayerCollisionSystem(gameData), Cycle.FixedUpdate);
            Add(new WeaponCollisionSystem(gameData), Cycle.FixedUpdate);
            Add(new ForceSystem(gameData), Cycle.FixedUpdate);
            Add(new RotationSystem(gameData), Cycle.FixedUpdate);
            Add(new MovementSystem(gameData), Cycle.FixedUpdate);
        }

        private void Add(System s, Cycle key) => systems[key].Add(s);

        void Update() => systems[Cycle.Update].ForEach(s => s.Update());
        void FixedUpdate() => systems[Cycle.FixedUpdate].ForEach(s => s.Update());

    }
}
