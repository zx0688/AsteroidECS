using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData
{
    public bool Failed = false;
    public bool Restart = false;
    public bool FirstStart = true;

    public int Score = 0;

    public bool DisableMovement => FirstStart || Failed;

}
