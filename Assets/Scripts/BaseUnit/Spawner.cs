using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : AliveObject
{
    [Header("Spawner")]
    private int ticksCount;
    [SerializeField] private int delay;
    [SerializeField] private int maxPopulation;
    [SerializeField] private GameObject botPrefab;
    public List<BotBase> population = new List<BotBase>();
    [SerializeField] private ParticleSystem pS;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float desactivationRadius;


    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Debug.Log(pS.shape.arcSpeed.constant);
        ParticleSystem.ShapeModule shape = pS.shape;
        shape.arcSpeed = Mathf.Lerp(0f, 5f, (ticksCount + 0f) / delay);
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        ticksCount = delay * maxPopulation;

    }

    private void FixedUpdate()
    {   
        if(GameManager.ClosestBot(!isEnemy, transform.position, desactivationRadius) == null)
        ticksCount++;
        if (ticksCount < delay) return; // every delay amount of tick
        ticksCount -= delay;
        PurgeList();
        if (population.Count >= maxPopulation) return; // try to spawn a unit if population is not cap

        float spawnAngle = Random.Range(0, 2 * Mathf.PI);
        Vector3 spawnPosition = new(transform.position.x + spawnRadius * Mathf.Cos(spawnAngle), transform.position.y, transform.position.z + spawnRadius * Mathf.Sin(spawnAngle));
        
        if (botPrefab.TryGetComponent<Swarmies>(out _))
        {
            GameObject swarmies = Instantiate(botPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(swarmies.GetComponent<Swarmies>().AddToSpawner(population));
        }
        else { 
            population.Add(GameManager.SpawnBot(botPrefab, spawnPosition));
        }
        
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
