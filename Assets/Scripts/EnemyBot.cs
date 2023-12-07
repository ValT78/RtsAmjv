using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBot : BotBase
{
    [SerializeField] private float VisionRange; // Définir la portée de vision des ennemis.
   
    void Update()
    {
        target = ClosestBot(GameManager.troopUnits.Select(troopBot => (BotBase)troopBot).ToList(), VisionRange);
        UpdateBehavior();
    }

    // Implémentation spécifique pour les ennemis.
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique spécifique aux ennemis, si nécessaire.
    }

   


}
