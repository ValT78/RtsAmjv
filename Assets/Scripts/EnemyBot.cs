using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : BotBase
{
    [SerializeField] private float VisionRange; // D�finir la port�e de vision des ennemis.
    private float cloasestRange;
   
    void Update()
    {
        target = FollowTroop(GameManager.troopUnits, VisionRange);
        UpdateBehavior();
    }

    // Impl�mentation sp�cifique pour les ennemis.
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique sp�cifique aux ennemis, si n�cessaire.
    }

    public BotBase FollowTroop(List<TroopBot> troupList, float maxRange)
    {
        float cloasestRange = maxRange;
        BotBase closestBot = null;
        // Logique pour d�tecter les troupes dans la visionRange.
        foreach (var troop in troupList)
        {
            float distanceToTroop = Vector3.Distance(transform.position, troop.transform.position);
            if (distanceToTroop <= cloasestRange)
            {
                // La troupe d�tect�e devient la nouvelle cible.
                closestBot = troop;
                cloasestRange = distanceToTroop;
            }
        }

        return closestBot;
    }


}
