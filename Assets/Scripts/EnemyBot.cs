using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBot : AttackController
{
    [SerializeField] private float VisionRange; // Définir la portée de vision des ennemis.
   
    // Implémentation spécifique pour les ennemis.
    public override void UpdateBehavior()
    {
        target = ClosestBot(GameManager.troopUnits.Select(troopBot => (AttackController)troopBot).ToList(), VisionRange);
        base.UpdateBehavior();
        // Logique spécifique aux ennemis, si nécessaire.
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = true;
    }


}
