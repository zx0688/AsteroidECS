using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public class UISystem : System
    {
        private UI ui;

        public UISystem(UI ui) : base()
        {
            this.ui = ui;
        }

        protected override bool Filter(GameObject entity) => HasComponents(entity, typeof(Player));

        override public void Update()
        {
            GameObject player = entities[0];

            Ammo a = player.GetComponent<Ammo>();
            ui.SetAmmo(a.Count, a.MaxCount);
            ui.SetReload(Mathf.Clamp01((Time.time - a.StartReloadTimestamp) / a.ReloadAmmoTime));

            Transform t = player.GetComponent<Transform>();
            ui.SetPosition(t.position);
            ui.SetAngle(t.rotation.eulerAngles.z);
            ui.SetVelocity(player.GetComponent<Velocity>().Value.magnitude);


            ui.SetGameOverVisible(player.GetComponent<HP>().Value == 0);

        }
    }
}