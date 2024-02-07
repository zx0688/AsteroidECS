using ECS;
using UnityEngine;

namespace ECS
{
    public class Ammo : MonoBehaviour, IDuration
    {
        public int Count = 0;
        public int MaxCount = 3;

        public float CooldownTime = 0.5f;
        public float StartCooldownTimestamp = 0f;

        public float StartReloadTimestamp = 0f;
        public float ReloadAmmoTime = 1f;


        public bool CooldownIsFinished(float currentTime) => currentTime - StartCooldownTimestamp >= CooldownTime;

        public bool IsFinished(float currentTime) => currentTime - StartReloadTimestamp >= ReloadAmmoTime;
    }
}