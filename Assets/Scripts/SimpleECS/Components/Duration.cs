using ECS;
using UnityEngine;

namespace ECS
{
    public class Duration : MonoBehaviour, IDuration
    {
        public float ActivateTime = 0f;
        public float DurationTime = 0.5f;

        public bool IsFinished(float currentTime) => currentTime - ActivateTime >= DurationTime;
    }
}