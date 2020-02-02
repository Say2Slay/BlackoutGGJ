using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public bool Serveuractivation;

    public Dictionary<string, bool> objective = new Dictionary<string, bool>();

    public float timer = 180f;
    public Text timerUI;

    void Start()
    {
        objective.Add("server", true);
        objective.Add("router", false);
        objective.Add("firewall", true);
        objective.Add("generator", false);
    }

    void Update()
    {
        float tempTimer = timer - Time.deltaTime;
        timerUI.text = tempTimer.ToString("00 : 00");

        if (Serveuractivation)
        {
        }
    }
}