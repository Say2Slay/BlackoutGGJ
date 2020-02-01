using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool isNearPlayer { get; set; }

    bool hasInteracted = false;

    Transform player;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (isNearPlayer && !hasInteracted)
        {
            Interact();
            hasInteracted = true;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Is interacting with " + transform.name);
    }
}
