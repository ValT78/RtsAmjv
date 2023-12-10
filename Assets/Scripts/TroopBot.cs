using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TroopBot : AttackController
{
    
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();

    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = false;
    }

}
