using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrap : TrapManager
{
    public override void Activate()
    {
        base.Activate();

        Teleport();
    }

    void Teleport()
    {
        //
        if (delay <= 0)
        {
            //
            Destroy(gameObject);
        }
    }
}
