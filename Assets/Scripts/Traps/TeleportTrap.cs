using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrap : TrapManager
{
    List<Transform> nextSpawn;

    public override void Activate()
    {
        base.Activate();

        SeekSpawn();
    }

    void SeekSpawn()
    {
        Transform Spawnlist = GameObject.FindGameObjectWithTag("Spawnlist").transform;
        Transform[] Spawners = Spawnlist.GetComponentsInChildren<Transform>();

        nextSpawn = new List<Transform>(Spawners);
        nextSpawn.RemoveAt(0);

        Teleport();
    }

    void Teleport()
    {
        int rand = Random.Range(0, nextSpawn.Count);
        Enemy.transform.position = nextSpawn[rand].position;

        Destroy(gameObject);
    }


}
