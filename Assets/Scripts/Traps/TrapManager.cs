using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public Transform Enemy;
    bool isTriggered = false;

    public float delay = 3f;

    public string trapperTag;

    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals(trapperTag))
        {
            isTriggered = true;
            Enemy = other.transform;
        }
    }

    void Update()
    {
        if (isTriggered)
        {
            Activate();
            delay -= Time.deltaTime;
        }
    }

    public virtual void Activate()
    {
        Debug.Log("The trap has been activated by " + Enemy.name);
    }
}
