using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    int scrollSelection, select;
    public RectTransform powers;
    public Image[] capacities;

    public bool Steuned;
    public string PlayerTag;

    Vector3 moveDir;

    void Awake()
    {
        cooldown = delay;
        powers = GameObject.FindGameObjectWithTag("powersList").GetComponent<RectTransform>();
        capacities = powers.GetComponentsInChildren<Image>();
        List<Image> capacitiesList = new List<Image>(capacities);
        capacitiesList.RemoveAt(0);
        capacities = capacitiesList.ToArray();
    }
    void Start()
    {
        cooldown = delay;
    }
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        InputMovement();
        scrollSelection = Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
        //Debug.Log(scrollSelection);
        InputCapacities();
    }

    private void InputMovement()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, 0, z).normalized * moveSpeed * speedBuffer * Time.deltaTime;
        CmdMove();
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        moveDir = new Vector3(x, 0, z).normalized * moveSpeed * speedBuffer * Time.deltaTime;

        if (x != 0 || z != 0)
            CmdMove();
    }

    private void InputCapacities()
    {
        if (scrollSelection < 0)
            select--;
        if (scrollSelection > 0)
            select++;
        if (select > capacities.Length - 1)
            select = 0;
        if (select < 0)
            select = capacities.Length - 1;
        for (int i = 0; i < capacities.Length; i++)
        {
            if (select == i)
            {
                capacities[i].color = Color.red;
            }
            else
            {
                capacities[i].color = Color.white;
            }
        }
    }
    [Command]
    void CmdMove()
    {
        RpcMovePlayer();
    }
    [ClientRpc]
    void RpcMovePlayer()
    {
        transform.Translate(moveDir, Space.World);

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