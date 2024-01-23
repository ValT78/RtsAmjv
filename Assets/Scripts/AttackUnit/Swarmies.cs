using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Swarmies : MonoBehaviour
{
    private List<BotBase> swarmies = new();
    [SerializeField] private GameObject swarmPrefab;
    [SerializeField] private int armySize;
    [SerializeField] private float armyRadius;

    private void Start()
    {
        swarmies.Add(GameManager.SpawnBot(swarmPrefab, transform.position));
        for (int spawnAngle = 1; spawnAngle < armySize; spawnAngle++)
        {
            swarmies.Add(GameManager.SpawnBot(swarmPrefab, new Vector3(transform.position.x + armyRadius * Mathf.Cos(2*Mathf.PI*spawnAngle/(armySize-1)),transform.position.y, transform.position.z + armyRadius * Mathf.Sin(2 * Mathf.PI * spawnAngle / (armySize-1)))));
        }
        
        foreach (BotBase swarm in swarmies)
        {
            swarm.SetIsSwarm(this);
        }
        
    }

    public void SetEveryTarget(AliveObject target)
    {
        foreach (BotBase swarm in swarmies)
        {
            if(swarm != null)
            {
                swarm.SetTarget(target, true);
            }
        }
    }

    public void SetEveryPosition(Vector3 position)
    {
        foreach (TroopBot swarm in swarmies.Cast<TroopBot>())
        {
            if (swarm != null)
            {
                swarm.GoToPosition(position, true);
            }
        }
    }

    public void SetEveryAware(Vector3 position)
    {
        foreach (TroopBot swarm in swarmies.Cast<TroopBot>())
        {
            if (swarm != null)
            {
                swarm.AwarePosition(position, true);
            }
        }
    }
    public void SelectEveryMode(bool state)
    {
        foreach (TroopBot swarm in swarmies.Cast<TroopBot>())
        {
            if (swarm != null)
            {
                swarm.SelectMode(state, true);
            }
        }
    }
    public void ShowEveryCapa(bool state)
    {
        foreach (TroopBot swarm in swarmies.Cast<TroopBot>())
        {
            if (swarm != null)
            {
                swarm.ShowCapa(state, true);
            }
        }
    }

    public bool RefillArmy()
    {
        bool flag = false;
        List<BotBase> newList = new();
        int spawnAngle = 0;
        foreach(BotBase swarm in swarmies)
        {
            if (swarm == null)
            {
                newList.Add(GameManager.SpawnBot(swarmPrefab, new Vector3(transform.position.x + armyRadius * Mathf.Cos(2 * Mathf.PI * spawnAngle / (armySize - 1)), transform.position.y, transform.position.z + armyRadius * Mathf.Sin(2 * Mathf.PI * spawnAngle / (armySize - 1)))));
                flag = true;
                spawnAngle++;
            }
            else newList.Add(swarm);
        }
        if (!flag) return false;
        swarmies = newList;
        foreach (BotBase swarm in swarmies)
        {
            swarm.attackController.SetSpecialUsed(true);
            swarm.SetIsSwarm(this);
            

        }
        return flag;
    }

    public IEnumerator AddToSpawner(List<BotBase> population)
    {
        while(swarmies.Count != armySize)
        {
            yield return null;
        }
        AddToSpawner2(population);
    }

    public void AddToSpawner2(List<BotBase> population)
    {
        foreach (BotBase swarm in swarmies)
        {
            if (swarm != null)
            {
                population.Add(swarm);
            }
        }
    }

}
