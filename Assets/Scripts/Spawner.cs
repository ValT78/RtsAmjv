using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : AliveObject
{
    private int ticksCount;
    [SerializeField] private int delay;
    [SerializeField] private int maxPopulation;
    [SerializeField] private GameObject botPrefab;
    public List<BotBase> population = new List<BotBase>();
    [SerializeField] private ParticleSystem pS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(pS.shape.arcSpeed.constant);
        ParticleSystem.ShapeModule shape = pS.shape;
        shape.arcSpeed = Mathf.Lerp(0f, 5f, (ticksCount + 0f) / delay);

    }

    private void FixedUpdate()
    {   
        ticksCount++;
        if (ticksCount < delay) return; // every delay amount of tick
        ticksCount -= delay;
        PurgeList();
        if (population.Count >= maxPopulation) return; // try to spawn a unit if population is not cap
        population.Add(GameManager.SpawnBot(botPrefab,transform.position));
    }

    private void PurgeList()
    {
        int i = 0;
        while(i < population.Count)
        {
            if (population[i] == null) population.RemoveAt(i);
            else i++;
        }
    }
}
