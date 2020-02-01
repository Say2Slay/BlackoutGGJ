using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Objective : NetworkBehaviour
{
    public GameManager gameManager;

    private int countdownCapture = 5;
    private float currentCountdownCapture;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        // TODO remove true, it is for test
        if (true || !gameManager.objective[other.tag])
        {
            if (Input.GetButton("Fire1"))
            {
                currentCountdownCapture += Time.deltaTime;
                if (currentCountdownCapture > countdownCapture)
                {
                    gameManager.objective[other.tag] = !gameManager.objective[other.tag];
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                currentCountdownCapture = 0;
            }
        }
    }

    private void captureObjective()
    {
    }

    private void checkObjectiveAlreadyCaptured()
    {
        if (this.tag.Equals("WhiteHat"))
        {
        }
        else
        {
        }
    }
}