using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : BotBase
{
    [SerializeField] private float VisionRange; // Définir la portée de vision des ennemis.
    private float cloasestRange;
   
    void Update()
    {
        target = FollowTroop(GameManager.troopUnits, VisionRange);
        UpdateBehavior();
    }

    // Implémentation spécifique pour les ennemis.
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique spécifique aux ennemis, si nécessaire.
    }

    public BotBase FollowTroop(List<TroopBot> troupList, float maxRange)
    {
        float cloasestRange = maxRange;
        BotBase closestBot = null;
        // Logique pour détecter les troupes dans la visionRange.
        foreach (var troop in troupList)
        {
            float distanceToTroop = Vector3.Distance(transform.position, troop.transform.position);
            if (distanceToTroop <= cloasestRange)
            {
                // La troupe détectée devient la nouvelle cible.
                closestBot = troop;
                cloasestRange = distanceToTroop;
            }
        }

        return closestBot;
    }


}
