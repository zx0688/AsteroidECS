using ECS;
using UnityEngine;

namespace ECS
{
    public class Cooldown : MonoBehaviour, IDuration
    {
        public float ActivateTime = 0f;
        public float DurationTime = 1f;

        public bool IsFinished(float currentTime) => currentTime - ActivateTime >= DurationTime;
    }
}