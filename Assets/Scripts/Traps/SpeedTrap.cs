using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrap : TrapManager
{
    public override void Activate()
    {
        base.Activate();

       Speed();
    }


    void Speed()
    {
        Enemy.GetComponent<CharacterBehaviour>().speedBuffer = 3;
        Enemy.GetComponent<CharacterBehaviour>().sprintBuffer = 4;

        if (delay <= 0)
        {
            Enemy.GetComponent<CharacterBehaviour>().speedBuffer = 1;
            Enemy.GetComponent<CharacterBehaviour>().sprintBuffer = 2;
            Destroy(gameObject);
        }
    }
}
