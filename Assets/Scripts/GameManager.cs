using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Listes statiques d'unités
    public static List<TroopBot> troopUnits = new();
    public static List<EnemyBot> enemyUnits = new();
    public static int deadEnemy;
    public static int deadAllie;
    public UIController UIController;



    private void Start()
    {
        // Récupérer toutes les unités de troupe dans la scène et les ajouter à la liste
        troopUnits.AddRange(FindObjectsOfType<TroopBot>().ToArray());

        // Récupérer toutes les unités d'ennemi dans la scène et les ajouter à la liste
        enemyUnits.AddRange(FindObjectsOfType<EnemyBot>().ToArray());

        // UIController.ActiveWinUI();
    }

    private void Update()
    {
        /*foreach (var t in troopUnits)
        {
            print(t.ToString());
        }*/
    }

    // Méthode pour invoquer un prefab de bot à une position spécifique
    public static BotBase SpawnBot(GameObject botPrefab, Vector3 position)
    {
        GameObject botObject = Instantiate(botPrefab, position, Quaternion.identity);
        
        if (botObject.TryGetComponent<EnemyBot>(out var enemy))
        {
            enemyUnits.Add(enemy);
            return enemy;
        }
        else if (botObject.TryGetComponent<TroopBot>(out var troop))
        {
            troopUnits.Add(troop);
            return troop;
        }
        return null;
    }

    public static void KillBot(GameObject botObject)
    {
        if (botObject.TryGetComponent<EnemyBot>(out var enemy))
        {
            enemyUnits.Remove(enemy);
            deadEnemy++;
        }
        else if (botObject.TryGetComponent<TroopBot>(out var troop))
        {
            print(troop.ToString());
            troopUnits.Remove(troop);
            deadAllie++;
        }
        Destroy(botObject);
    }

    public static List<BotBase> GetCrewBots(bool isEnemy)
    {
        if(isEnemy)
        {
            return enemyUnits.Select(enemyBot => (BotBase)enemyBot).ToList();
        }
        else
        {
            return troopUnits.Select(troopBot => (BotBase)troopBot).ToList();
        }
    }

    public static BotBase ClosestBot(bool isEnemy, Vector3 position, float maxRange)
    {
        List<BotBase> botList = GetCrewBots(isEnemy);

        if (botList.Count == 0) return null;
        float cloasestRange = maxRange;
        BotBase closestBot = null;
        // Logique pour d�tecter les troupes dans la visionRange.
        foreach (var troop in botList)
        {
            if(troop == null) continue;
            float distanceToTroop = Vector3.Distance(position, troop.transform.position);

            if (distanceToTroop <= cloasestRange)
            {
                // La troupe détectee devient la nouvelle cible.
                closestBot = troop;
                cloasestRange = distanceToTroop;
            }
        }

        return closestBot;
    }

}
