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
        Enemy.GetComponent<CharacterBehaviour>().Inverted = true;

        if (delay <= 0)
        {
            Enemy.GetComponent<CharacterBehaviour>().Inverted = false;
            Destroy(gameObject);
        }
    }
}
