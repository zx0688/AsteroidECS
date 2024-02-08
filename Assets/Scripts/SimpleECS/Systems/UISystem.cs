using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;

namespace ECS
{
    public class UISystem : System
    {
        private UI ui;
        private Player player;
        private Ammo ammo;
        private Transform transform;
        private GameObject ship;

        public UISystem(UI ui, GameData gameData) : base(gameData)
        {
            this.ui = ui;
        }

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Player));

        protected override void OnEntitiesChanged()
        {
            base.OnEntitiesChanged();
            ship = entities.FirstOrDefault();
            player = ship.GetComponent<Player>();
            ammo = ship.GetComponent<Ammo>();
            transform = ship.GetComponent<Transform>();
        }

        override public void Update()
        {
            ui.SetAmmo(ammo.Count, ammo.MaxCount);
            ui.SetReload(Mathf.Clamp01((Time.time - ammo.StartReloadTimestamp) / ammo.ReloadAmmoTime));

            ui.SetPosition(transform.position);
            ui.SetAngle(transform.rotation.eulerAngles.z);
            ui.SetVelocity(player.GetComponent<Velocity>().Value.magnitude);
            ui.SetScore(gameData.Score);

            ui.SetGameOverVisible(gameData.Failed);
            ui.SetStartPanelVisible(gameData.FirstStart);
        }
    }
}