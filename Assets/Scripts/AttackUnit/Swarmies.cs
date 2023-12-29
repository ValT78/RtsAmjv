using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Swarmies : MonoBehaviour
{
    private List<BotBase> swarmies = new();
    [SerializeField] private GameObject swarmPrefab;

    private void Start()
    {
        swarmies.Add(GameManager.SpawnBot(swarmPrefab, transform.position));
        swarmies.Add(GameManager.SpawnBot(swarmPrefab, transform.position));
        swarmies.Add(GameManager.SpawnBot(swarmPrefab, transform.position));
        swarmies.Add(GameManager.SpawnBot(swarmPrefab, transform.position));
        swarmies.Add(GameManager.SpawnBot(swarmPrefab, transform.position));
        swarmies.Add(GameManager.SpawnBot(swarmPrefab, transform.position));
        foreach (var swarm in swarmies)
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

    public bool RefillArmy()
    {
        bool flag = false;
        List<BotBase> newList = new List<BotBase>();
        foreach(BotBase swarm in swarmies)
        {
            if (swarm == null)
            {
                newList.Add(GameManager.SpawnBot(swarmPrefab, new(0, 0, 0)));
                flag = true;
            }
            else newList.Add(swarm);
        }
        if (!flag) return false;
        swarmies = newList;
        foreach (BotBase swarm in swarmies)
        {
            swarm.GetComponent<Swarmy>().SetSpecialUsed(true);
            swarm.SetIsSwarm(this);

        }
        return flag;
    }

}
