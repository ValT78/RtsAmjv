using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBot : BotBase
{
   
    // Impl�mentation sp�cifique pour les ennemis.
    public override void UpdateBehavior()
    {
        target = ClosestBot(GameManager.troopUnits.Select(troopBot => (BotBase)troopBot).ToList(), awareRange);
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
