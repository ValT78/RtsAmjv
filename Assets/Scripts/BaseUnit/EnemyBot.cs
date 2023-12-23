using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBot : BotBase
{

    public override void UpdateBehavior()
    {
        SetTarget(
            GameManager.ClosestBot(!isEnemy,transform.position, awareRange)
            );
        base.UpdateBehavior();
        // Logique sp�cifique aux ennemis, si n�cessaire.
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = true;
        isAware = true;
    }
    

}
