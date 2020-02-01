using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class UI : NetworkBehaviour
{
    GameObject localPlayer;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
            localPlayer = GameObject.FindGameObjectWithTag("Player");

        if (localPlayer == null) 
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }
    }
}
