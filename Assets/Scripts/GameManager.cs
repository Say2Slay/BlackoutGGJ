using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public bool Serveuractivation;

    public Dictionary<string, bool> objective = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        objective.Add("server", true);
        objective.Add("router", false);
        objective.Add("firewall", true);
        objective.Add("generator", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Serveuractivation)
        {
        }
    }
}