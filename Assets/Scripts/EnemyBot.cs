using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : BotBase
{
    [SerializeField] private float VisionRange; // D�finir la port�e de vision des ennemis.
   
    void Update()
    {
        DetectTroups(GameManager.troopUnits);
        UpdateBehavior();
    }

    // Impl�mentation sp�cifique pour les ennemis.
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique sp�cifique aux ennemis, si n�cessaire.
    }

    public void DetectTroups(List<TroopBot> troupList)
    {
        // Logique pour d�tecter les troupes dans la visionRange.
        foreach (var troop in troupList)
        {
            float distanceToTroop = Vector3.Distance(transform.position, troop.transform.position);
            if (distanceToTroop <= VisionRange)
            {
                // La troupe d�tect�e devient la nouvelle cible.
                target = troop.gameObject;
            }
        }
    }

    
}
