using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBot : BotBase
{
   
    // Implémentation spécifique pour les ennemis.
    public override void UpdateBehavior()
    {
        target = ClosestBot(GameManager.troopUnits.Select(troopBot => (BotBase)troopBot).ToList(), awareRange);
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
