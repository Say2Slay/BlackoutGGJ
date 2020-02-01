using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class CameraBehaviour : NetworkBehaviour
{
    Transform Target;
    public float smoothSpeed = 10f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            Vector3 spot = Target.position + offset;
            Vector3 smoothMove = Vector3.Lerp(transform.position, spot, smoothSpeed * Time.deltaTime);
            transform.position = smoothMove;

            transform.LookAt(Target);
        }
    }
}
