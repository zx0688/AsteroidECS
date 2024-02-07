using System;
using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECS
{
    public class InputSystem : System
    {
        private DefPlayerActions playerActions;
        private InputAction move;

        private GameObject player;

        protected override void Init()
        {
            base.Init();

            playerActions = new DefPlayerActions();
            playerActions.ship.Fire.Enable();
            playerActions.ship.Move.Enable();
            playerActions.ship.Laser.Enable();

            move = playerActions.ship.Move;

            playerActions.ship.Fire.performed += OnFire;
            playerActions.ship.Laser.performed += OnLaserShoot;
        }

        override protected void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();
            player = GetEntities(typeof(PlayerInput))[0];
        }

        private void OnLaserShoot(InputAction.CallbackContext contect)
        {
            if (!player.TryGetComponent(out Ammo cd) || cd.Count == 0 || !cd.CooldownIsFinished(Time.time))
                return;

            cd.Count -= 1;
            cd.StartCooldownTimestamp = Time.time;
            cd.StartReloadTimestamp = Time.time;

            Transform tp = player.GetComponent<Transform>();
            Vector3 playerDirection = tp.TransformDirection(Vector3.up);
            GameObject laser = Creator.Create("Laser");
            laser.GetComponent<Transform>().parent = tp;
            laser.GetComponent<Transform>().position = tp.position + playerDirection / 2f;
            laser.GetComponent<Transform>().rotation = tp.rotation;

            AddEntity(laser);

        }

        private void OnFire(InputAction.CallbackContext contect)
        {
            Transform tp = player.GetComponent<Transform>();
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
            Vector2 moveValue = move.ReadValue<Vector2>();
            player.GetComponent<Force>().Value = moveValue.y * 2;

            if (moveValue.x != 0)
            {
                Transform t = player.GetComponent<Transform>();
                var rot = t.rotation.eulerAngles;
                rot.z -= moveValue.x;
                t.rotation = Quaternion.Euler(rot);
            }

        }
    }
}