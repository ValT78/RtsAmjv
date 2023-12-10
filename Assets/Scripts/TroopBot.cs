using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopBot : AttackController
{

    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique spécifique aux troupes, si nécessaire.
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = false;
    }

}
