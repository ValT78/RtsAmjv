using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBot : BotBase
{
    [SerializeField] private float VisionRange; // D�finir la port�e de vision des ennemis.
   
    void Update()
    {
        target = ClosestBot(GameManager.troopUnits.Select(troopBot => (BotBase)troopBot).ToList(), VisionRange);
        UpdateBehavior();
    }

    // Impl�mentation sp�cifique pour les ennemis.
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique sp�cifique aux ennemis, si n�cessaire.
    }

   


}
