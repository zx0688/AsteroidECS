using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECS
{
    public class InputSystem : System
    {
        private DefPlayerActions playerActions;
        private InputAction move;

        private GameObject ship;
        private Player player;

        public InputSystem(GameData gameData) : base(gameData)
        {
        }

        protected override void Init()
        {
            base.Init();

            playerActions = new DefPlayerActions();
            playerActions.ship.Fire.Enable();
            playerActions.ship.Move.Enable();
            playerActions.ship.Laser.Enable();

            move = playerActions.ship.Move;

            playerActions.ship.Fire.performed += OnFire;
            playerActions.ship.Fire.performed += OnRestart;

            playerActions.ship.Laser.performed += OnLaserShoot;
        }

        override protected void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();

            ship = GetEntities(typeof(PlayerInput)).FirstOrDefault();
            player = ship.GetComponent<Player>();
        }

        private void OnLaserShoot(InputAction.CallbackContext contect)
        {
            if (!ship.TryGetComponent(out Ammo cd) || cd.Count == 0 || !cd.CooldownIsFinished(Time.time) || gameData.Failed)
                return;

            cd.Count -= 1;
            cd.StartCooldownTimestamp = Time.time;
            cd.StartReloadTimestamp = Time.time;

            Transform tp = ship.GetComponent<Transform>();
            Vector3 playerDirection = tp.TransformDirection(Vector3.up);
            GameObject laser = Creator.Create("Laser");
            laser.GetComponent<Transform>().parent = tp;
            laser.GetComponent<Transform>().position = tp.position + playerDirection / 2f;
            laser.GetComponent<Transform>().rotation = tp.rotation;

            AddEntity(laser);

        }

        private void OnRestart(InputAction.CallbackContext contect)
        {
            gameData.FirstStart = false;

            if (!gameData.Failed)
                return;
            gameData.Restart = true;
        }

        private void OnFire(InputAction.CallbackContext contect)
        {
            if (gameData.Failed || gameData.FirstStart)
                return;

            Transform tp = ship.GetComponent<Transform>();
            Vector3 playerDirection = tp.TransformDirection(Vector3.up);

            GameObject bullet = Creator.Create("Bullet");
            bullet.GetComponent<Transform>().position = tp.position + playerDirection / 2f;
            bullet.GetComponent<Velocity>().Value = 5f * playerDirection;
            bullet.GetComponent<Velocity>().MaxValue = 3f;
            bullet.GetComponent<Transform>().rotation = tp.rotation;
            AddEntity(bullet);
        }

        override public void Update()
        {
            if (gameData.Failed)
                return;

            Vector2 moveValue = move.ReadValue<Vector2>();
            ship.GetComponent<Force>().Value = moveValue.y * 2;

            if (moveValue.x != 0)
            {
                Transform t = ship.GetComponent<Transform>();
                var rot = t.rotation.eulerAngles;
                rot.z -= moveValue.x;
                t.rotation = Quaternion.Euler(rot);
            }

        }
    }
}