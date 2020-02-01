using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class CharacterBehaviour : NetworkBehaviour
{
    public float moveSpeed, rotationSpeed;
    public float speedBuffer = 1;
    public float sprintBuffer = 2;
    private float x, z;

    public float delay = 10f;
    float cooldown;

    public int Team;
    int scrollSelection, select;

    public RectTransform powers;
    public Image[] capacities;
    public Image StaminaBar, UltiIcon;

    public bool Stunned, Inverted; // traps effects booleans
    public bool playerhat; //bool true = WhiteHat; bool false = BlackHat

    public string PlayerTag;

    public float Stamina = 100;
    public bool Run;
    public bool StaminaLock;

    private float StaminaDecrease = 40f;
    private float StaminaIncrease = 30f;

    Vector3 moveDir;
    public Vector3 lookRotation = Vector3.forward;

    void Awake()
    {
        StaminaBar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<Image>();
        UltiIcon = GameObject.FindGameObjectWithTag("UltiIcon").GetComponent<Image>();

        cooldown = delay;

        powers = GameObject.FindGameObjectWithTag("powersList").GetComponent<RectTransform>();
        capacities = powers.GetComponentsInChildren<Image>();
    }
    void Start()
    {
        cooldown = delay;
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (!Run && Stamina < 100)
        {

            Stamina += StaminaIncrease * Time.deltaTime;
            if (Stamina >= 100)
            {
                Stamina = 100;
                StaminaLock = false;
            }
        }

        Run = Input.GetButtonUp("Sprint");

        if (!Stunned)
        {
            InputMovement();
            Rotate();
            InputCapacities();
        }

        if (Input.GetButtonDown("Ulti"))
        {
            if (!playerhat && cooldown <= 0)
                StartCoroutine(Noclip());
        }

        cooldown -= Time.deltaTime;

        StaminaBar.fillAmount = Stamina / 100f;
        UltiIcon.fillAmount = cooldown / 10f;
    }

    private void InputMovement()
    {
        if (!Inverted)
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
        } else
        {
            x = -Input.GetAxisRaw("Horizontal");
            z = -Input.GetAxisRaw("Vertical");
        }

        if (Input.GetButtonUp("Sprint"))
        {
            speedBuffer = 1;
        }
        else if (Input.GetButton("Sprint") && (x != 0 || z != 0))
        {
            if (Stamina > 0 && !StaminaLock)
            {
                Stamina -= StaminaDecrease * Time.deltaTime;
                speedBuffer = sprintBuffer;
                Run = true;
            }
            else if (!StaminaLock)
            {
                Stamina = 0;
                StaminaLock = true;
                speedBuffer = 1;
            }

            if (Stamina >= 100)
            {
                Stamina = 100;
                StaminaLock = false;
            }
        }

        moveDir = new Vector3(x, 0, z).normalized * moveSpeed * speedBuffer * Time.deltaTime;
        lookRotation = moveDir;

        if (x != 0 || z != 0)
            CmdMove();
    }

    void Rotate()
    {
        if (lookRotation != Vector3.zero)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), .3f);
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

    public void ChoiceHat()
    {
        if (playerhat)
        {
            transform.gameObject.tag = "WhiteHat";
        }
        else if (!playerhat)
        {
            transform.gameObject.tag = "BlackHat";
        }

        PlayerTag = transform.gameObject.tag;
    }

    private void InputCapacities()
    {
        scrollSelection = Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
        if (scrollSelection < 0)
            select--;

        if (scrollSelection > 0)
            select++;

        if (select > capacities.Length)
            select = 0;

        if (select < 0)
            select = capacities.Length;

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
}