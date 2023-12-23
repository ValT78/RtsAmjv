using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmy : AttackController
{

    public override void StartBehavior()
    {
        base.StartBehavior();
    }

    public override void UpdateBehavior()
    {

    }

    public override void SpecialAttack(Vector3 targetPosition)
    {
        botBase.GetIsSwarm().RefillArmy();
    }
}
