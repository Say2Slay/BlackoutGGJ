using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class CharacterBehaviour : NetworkBehaviour
{
    public float moveSpeed;
    public float speedBuffer = 1;
    private float x, z;

    public float delay = 10f;
    float cooldown;

    public int Team;

    void Start()
    {
        cooldown = delay;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        InputMovement();
    }

    private void InputMovement()
    {
        x = Input.GetAxis("Horizontal") * moveSpeed * speedBuffer * Time.deltaTime;
        z = Input.GetAxis("Vertical") * moveSpeed * speedBuffer * Time.deltaTime;

        CmdMove();
    }

    [Command]
    void CmdMove()
    {
        RpcJumpPlayer();
    }

    [ClientRpc]
    void RpcJumpPlayer()
    {
        transform.Translate(x, 0, z);

        if (Input.GetButtonDown("Ulti"))
        {
            if (Team == 1 && cooldown <= 0)
                StartCoroutine(Noclip());
        }

        cooldown -= Time.deltaTime;
    }

    IEnumerator Noclip()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        yield return new WaitForSeconds(5f);

        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;

        cooldown = delay;
    }
}