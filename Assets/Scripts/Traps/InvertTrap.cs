using UnityEngine;

public class InvertTrap : TrapManager
{
    public override void Activate()
    {
        base.Activate();

        Invert();
    }


    void Invert()
    {
        float xInverted = -Input.GetAxis("Horizontal");
        float zInverted = -Input.GetAxis("Vertical");

        float enemySpeed = Enemy.GetComponent<CharacterBehaviour>().moveSpeed;
        float enemyBuffer = Enemy.GetComponent<CharacterBehaviour>().speedBuffer;
        Vector3 invertedMoveDir = new Vector3(xInverted, 0, zInverted).normalized * enemySpeed * enemyBuffer * Time.deltaTime;

        //Écrire ici l'inversion des contrôles

        if (delay <= 0)
        {
            //Écrire ici le retour à la normale
            Destroy(gameObject);
        }
    }
}
