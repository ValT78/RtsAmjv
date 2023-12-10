using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Listes statiques d'unités
    public static List<TroopBot> troopUnits = new();
    public static List<EnemyBot> enemyUnits = new();

    private void Start()
    {
        // Récupérer toutes les unités de troupe dans la scène et les ajouter à la liste
        TroopBot[] troopsInScene = GameObject.FindObjectsOfType<TroopBot>().ToArray();
        troopUnits.AddRange(troopsInScene);

        // Récupérer toutes les unités d'ennemi dans la scène et les ajouter à la liste
        EnemyBot[] enemiesInScene = GameObject.FindObjectsOfType<EnemyBot>().ToArray();
        enemyUnits.AddRange(enemiesInScene);
    }


    // Méthode pour invoquer un prefab de bot à une position spécifique
    public static void SpawnBot(GameObject botPrefab, Vector3 position)
    {
        GameObject botObject = Instantiate(botPrefab, position, Quaternion.identity);
        
        if (botObject.TryGetComponent<EnemyBot>(out var enemy))
        {
            enemyUnits.Add(enemy);
        }
        else if (botObject.TryGetComponent<TroopBot>(out var troop))
        {
            troopUnits.Add(troop);
        }
    }

    public static void KillBot(GameObject botObject)
    {
        if (botObject.TryGetComponent<EnemyBot>(out var enemy))
        {
            enemyUnits.Remove(enemy);
        }
        else if (botObject.TryGetComponent<TroopBot>(out var troop))
        {
            troopUnits.Remove(troop);
        }
        Destroy(botObject);
    }

    public static List<AttackController> GetCrewBots(bool isEnemy)
    {
        if(isEnemy)
        {
            return enemyUnits.Select(enemyBot => (AttackController)enemyBot).ToList();
        }
        else
        {
            return troopUnits.Select(troopBot => (AttackController)troopBot).ToList();
        }
    }
}
