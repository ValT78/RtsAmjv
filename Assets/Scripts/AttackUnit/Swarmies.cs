using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Swarmies : MonoBehaviour
{
    [SerializeField] private List<BotBase> swarmies;
    [SerializeField] private GameObject swarmPrefab;

    private void Start()
    {
        foreach (TroopBot swarm in swarmies.Cast<TroopBot>())
        {
            GameManager.troopUnits.Add(swarm);

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

    public void RefillArmy()
    {
        foreach(BotBase swarm in swarmies)
        {
            if(swarm == null)
            {
                swarmies.Remove(swarm);
                swarmies.Add(GameManager.SpawnBot(swarmPrefab, new(0, 0, 0)));
            }
        }
        foreach (BotBase swarm in swarmies)
        {
            GetComponent<Swarmy>().SetSpecialUsed(true);
        }
    }

}
