using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{

    public class Core : MonoBehaviour
    {
        [SerializeField] private UI ui;

        private static Dictionary<Cycle, List<System>> systems = new Dictionary<Cycle, List<System>>();

        private enum Cycle
        {
            Update,
            FixedUpdate
        }

        void Start()
        {
            systems.Add(Cycle.Update, new List<System>());
            systems.Add(Cycle.FixedUpdate, new List<System>());


            Add(new CreateSystem(), Cycle.Update);
            Add(new InputSystem(), Cycle.Update);
            Add(new PortalSystem(), Cycle.Update);
            Add(new HPSystem(), Cycle.Update);
            Add(new FollowSystem(), Cycle.Update);
            Add(new DurationSystem(), Cycle.Update);
            Add(new AmmoSystem(), Cycle.Update);
            Add(new UISystem(ui), Cycle.Update);

            Add(new PlayerCollisionSystem(), Cycle.FixedUpdate);
            Add(new WeaponCollisionSystem(), Cycle.FixedUpdate);
            Add(new ForceSystem(), Cycle.FixedUpdate);
            Add(new RotationSystem(), Cycle.FixedUpdate);
            Add(new MovementSystem(), Cycle.FixedUpdate);
            Add(new PositionSystem(), Cycle.FixedUpdate);


        }

        private void Add(System s, Cycle key) => systems[key].Add(s);

        void Update() => systems[Cycle.Update].ForEach(s => s.Update());
        void FixedUpdate() => systems[Cycle.FixedUpdate].ForEach(s => s.Update());

    }
}
