using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBot : AttackController
{
    [SerializeField] private float VisionRange; // D�finir la port�e de vision des ennemis.
   
    // Impl�mentation sp�cifique pour les ennemis.
    public override void UpdateBehavior()
    {
        target = ClosestBot(GameManager.troopUnits.Select(troopBot => (AttackController)troopBot).ToList(), VisionRange);
        base.UpdateBehavior();
        // Logique sp�cifique aux ennemis, si n�cessaire.
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = true;
    }


}
