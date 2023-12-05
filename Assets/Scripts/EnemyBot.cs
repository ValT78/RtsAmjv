using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : BotBase
{
    [SerializeField] private float VisionRange; // Définir la portée de vision des ennemis.
   
    void Update()
    {
        DetectTroups(GameManager.troopUnits);
        UpdateBehavior();
    }

    // Implémentation spécifique pour les ennemis.
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique spécifique aux ennemis, si nécessaire.
    }

    public void DetectTroups(List<TroopBot> troupList)
    {
        // Logique pour détecter les troupes dans la visionRange.
        foreach (var troop in troupList)
        {
            float distanceToTroop = Vector3.Distance(transform.position, troop.transform.position);
            if (distanceToTroop <= VisionRange)
            {
                // La troupe détectée devient la nouvelle cible.
                target = troop.gameObject;
            }
        }
    }

    
}
