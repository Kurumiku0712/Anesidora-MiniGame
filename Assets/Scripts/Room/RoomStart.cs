using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStart : Room
{
    public override void Initialization()
    {
        base.Initialization();
    }
    public override void PlayerEnter()
    {
        base.PlayerEnter();
        isExplored = true;
    }
}
