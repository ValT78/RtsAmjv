using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopBot : AttackController
{

    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique sp�cifique aux troupes, si n�cessaire.
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = false;
    }

}
