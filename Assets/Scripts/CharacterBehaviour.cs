using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float moveSpeed;
    public float speedBuffer = 1;
    private float x, z;

    void Start()
    {
        
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal") * moveSpeed * speedBuffer * Time.deltaTime;
        z = Input.GetAxis("Vertical") * moveSpeed * speedBuffer * Time.deltaTime;

        transform.Translate(x, 0, z);
    }
}
