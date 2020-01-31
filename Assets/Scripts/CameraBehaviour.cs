using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Transform Target;
    public float smoothSpeed = 10f;
    public Vector3 offset;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        Vector3 spot = Target.position + offset;
        Vector3 smoothMove = Vector3.Lerp(transform.position, spot, smoothSpeed * Time.deltaTime);
        transform.position = smoothMove;

        transform.LookAt(Target);
    }
}
