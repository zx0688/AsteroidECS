using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace ECS
{
    public interface IDuration
    {
        bool IsFinished(float currentTime);
    }
}