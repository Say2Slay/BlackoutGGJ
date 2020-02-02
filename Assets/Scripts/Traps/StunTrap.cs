using UnityEngine;

public class StunTrap : TrapManager
{
    public override void Activate()
    {
        base.Activate();

        Stun();
    }

    void Stun()
    {
        Enemy.GetComponent<CharacterBehaviour>().Stunned = true;
        if (delay <= 0)
        {
            Enemy.GetComponent<CharacterBehaviour>().Stunned = false;
            Destroy(gameObject);
        }
    }
}
