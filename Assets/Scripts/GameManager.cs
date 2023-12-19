using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Listes statiques d'unit�s
    public static List<TroopBot> troopUnits = new();
    public static List<EnemyBot> enemyUnits = new();
    public static int deadEnemy;
    public static int deadAllie;
    public UIController UIController;



    private void Start()
    {
        // R�cup�rer toutes les unit�s de troupe dans la sc�ne et les ajouter � la liste
        TroopBot[] troopsInScene = GameObject.FindObjectsOfType<TroopBot>().ToArray();
        troopUnits.AddRange(troopsInScene);

        // R�cup�rer toutes les unit�s d'ennemi dans la sc�ne et les ajouter � la liste
        EnemyBot[] enemiesInScene = GameObject.FindObjectsOfType<EnemyBot>().ToArray();
        enemyUnits.AddRange(enemiesInScene);

        // UIController.ActiveWinUI();
    }


    // M�thode pour invoquer un prefab de bot � une position sp�cifique
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
            deadEnemy++;
        }
        else if (botObject.TryGetComponent<TroopBot>(out var troop))
        {
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
}
