using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Listes statiques d'unités
    public static List<TroopBot> troopUnits = new();
    public static List<AliveObject> troopObjects = new();
    public static List<EnemyBot> enemyUnits = new();
    public static List<AliveObject> enemyObjects = new();
    public static int deadEnemy;
    public static int deadAllie;
    public static bool isChosingTroop;
    public UIController UIController;
    public static bool playerAttack;



    private void Start()
    {
        // Récupérer toutes les unités de troupe dans la scène et les ajouter à la liste
        troopUnits.AddRange(FindObjectsOfType<TroopBot>().ToArray());
        // Récupérer toutes les unités d'ennemi dans la scène et les ajouter à la liste
        enemyUnits.AddRange(FindObjectsOfType<EnemyBot>().ToArray());

        foreach (AliveObject obj in FindObjectsOfType<AliveObject>()) { 
            if(obj.isEnemy) enemyObjects.Add(obj);
            else troopObjects.Add(obj);
            print(obj.name);
        }
        playerAttack = false;


        // UIController.ActiveWinUI();
    }

    // Méthode pour invoquer un prefab de bot à une position spécifique
    public static AliveObject SpawnAlive(GameObject botPrefab, Vector3 position)
    {
        GameObject objectPrefab = Instantiate(botPrefab, position, Quaternion.identity);
        AliveObject aliveObject = objectPrefab.GetComponent<AliveObject>();
        if (aliveObject.isEnemy)
        {
            enemyObjects.Add(aliveObject);
        }
        else
        {
            troopObjects.Add(aliveObject);
        }
        return aliveObject;
    }

    public static BotBase SpawnBot(GameObject botPrefab, Vector3 position)
    {
        GameObject objectPrefab = Instantiate(botPrefab, position, Quaternion.identity);
        AliveObject aliveObject = objectPrefab.GetComponent<AliveObject>();
        if (objectPrefab.TryGetComponent(out EnemyBot enemy))
            {
                enemyUnits.Add(enemy);
                enemyObjects.Add(aliveObject);
            }
        else if (objectPrefab.TryGetComponent(out TroopBot troop))
            {
                troopUnits.Add(troop);
                troopObjects.Add(aliveObject);

            }
        return objectPrefab.GetComponent<BotBase>();
    }

    public static void KillBot(GameObject botObject)
    {
        AliveObject aliveObject = botObject.GetComponent<AliveObject>();

        if (aliveObject.isEnemy)
        {
            enemyObjects.Remove(aliveObject);

            if (botObject.TryGetComponent(out EnemyBot enemyBot))
            {
                enemyUnits.Remove(enemyBot);
                deadEnemy++;

            }
        }
        else
        {
            troopObjects.Remove(aliveObject);
            if (botObject.TryGetComponent(out TroopBot troopBot))
            {
                deadAllie++;
                troopUnits.Remove(troopBot);
            }
        }
        Destroy(botObject);
    }

    public static List<BotBase> GetCrewBot(bool isEnemy)
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

    public static List<AliveObject> GetCrewAlive(bool isEnemy)
    {
        if (isEnemy)
        {
            return enemyObjects;
        }
        else
        {
            return troopObjects;
        }
    }

    public static AliveObject ClosestAlive(bool isEnemy, Vector3 position, float maxRange)
    {
        List<AliveObject> aliveList = GetCrewAlive(isEnemy);

        if (aliveList.Count == 0) return null;
        float cloasestRange = maxRange;
        AliveObject closestAlive = null;
        // Logique pour d�tecter les troupes dans la visionRange.
        foreach (AliveObject troop in aliveList)
        {
            if (troop != null)
            {
                float distanceToTroop = Vector3.Distance(position, troop.transform.position);

                if (distanceToTroop <= cloasestRange)
                {
                    // La troupe détectee devient la nouvelle cible.
                    closestAlive = troop;
                    cloasestRange = distanceToTroop;
                }
            }
            else aliveList.Remove(troop);
        }

        return closestAlive;
    }

    public static BotBase ClosestBot(bool isEnemy, Vector3 position, float maxRange)
    {
        List<BotBase> botList = GetCrewBot(isEnemy);

        if (botList.Count == 0) return null;
        float cloasestRange = maxRange;
        BotBase closestBot = null;
        // Logique pour d�tecter les troupes dans la visionRange.
        foreach (var troop in botList)
        {
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
