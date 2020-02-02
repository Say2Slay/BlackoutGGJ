using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public bool Serveuractivation;

    public Dictionary<string, bool> objective = new Dictionary<string, bool>();

    [SyncVar]
    public float timer;
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
        timer = timer - Time.deltaTime;
        timerUI.text = timer.ToString("0 00");

        if (timer <= 0)
        {
            timer = 0;
            Endgame();
        }

        if (Serveuractivation)
        {
        }
    }

    void Endgame()
    {
        Time.timeScale = 0;
        //Meilleures conditions de fin de partie à mettre
    }
}