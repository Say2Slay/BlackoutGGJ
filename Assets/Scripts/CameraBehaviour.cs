using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class CameraBehaviour : NetworkBehaviour
{
    public Transform Target;
    public float smoothSpeed = 10f;
    public Vector3 offset;

    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (Target.GetComponent<NetworkBehaviour>().isLocalPlayer)
        {
            Vector3 spot = Target.position + offset;
            Vector3 smoothMove = Vector3.Lerp(transform.position, spot, smoothSpeed * Time.deltaTime);
            transform.position = smoothMove;

            transform.LookAt(Target);
        }
    }
}
