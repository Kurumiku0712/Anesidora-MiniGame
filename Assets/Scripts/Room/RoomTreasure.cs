using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTreasure : Room
{
    public override void PlayerEnter()
    {
        base.PlayerEnter();
        isExplored = true;
    }
}
