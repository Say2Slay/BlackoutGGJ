using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class Detection : NetworkBehaviour
{
    public Transform otherPlayer;
    public float maxAngle, maxRadius;
    private bool isInFov = false;

    private void OnDrawGizmos()
    {
        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }

    public static bool inFOV(Transform CheckingObject, Transform Target, float maxAngle, float maxRadius)
    {
        Collider[] Overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(CheckingObject.position, maxRadius, Overlaps);

        for (int i = 0; i < count; i++)
        {
            if (Overlaps[i] != null)
            {
                if (Overlaps[i].transform == Target)
                {
                    Vector3 directionBetween = (Target.position - CheckingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(CheckingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(CheckingObject.position, Target.position - CheckingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == Target)
                                return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        isInFov = inFOV(transform, otherPlayer, maxAngle, maxRadius);

        if (!isInFov)
        {
            Debug.Log("Enemy not visible");
        } else
        {
            Debug.Log("Enemy visible");
        }
    }
}
