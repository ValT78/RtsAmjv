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
        // Logique spécifique aux ennemis, si nécessaire.
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = true;
        isAware = true;
    }
    

}
