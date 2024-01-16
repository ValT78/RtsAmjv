using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCard : MonoBehaviour
{
    [Header("ExploreMap")]
    [SerializeField] private GameObject retractMenu;


    [Header("SpawnPoint")]
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float spawnRadius;
    [Header("Prefab Unit")]
    [SerializeField] private GameObject healerPrefab;
    [SerializeField] private GameObject assassinPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject sorcierPrefab;
    [SerializeField] private GameObject swarmiesPrefab;
    [SerializeField] private GameObject sniperPrefab;
    [Header("Unit Counter")]
    [SerializeField] private TextMeshProUGUI capacityText;
    [SerializeField] private TextMeshProUGUI healerText;
    [SerializeField] private TextMeshProUGUI assassinText;
    [SerializeField] private TextMeshProUGUI tankText;
    [SerializeField] private TextMeshProUGUI sorcierText;
    [SerializeField] private TextMeshProUGUI swarmiesText;
    [SerializeField] private TextMeshProUGUI sniperText;
    [Header("Unit Capacity")]
    [SerializeField] private int capacity;
    [SerializeField] private int healerCoast;
    [SerializeField] private int assassinCoast;
    [SerializeField] private int tankCoast;
    [SerializeField] private int sorcierCoast;
    [SerializeField] private int swarmiesCoast;
    [SerializeField] private int sniperCoast;
    private int healerCount;
    private int assassinCount;
    private int tankCount;
    private int sorcierCount;
    private int swarmiesCount;
    private int sniperCount;

    private void Start()
    {
        capacityText.text= "Capacity\n\n" + capacity.ToString();
    }

    private void OnEnable()
    {
        GameManager.isChosingTroop = true;
    }
    private void OnDisable()
    {
        GameManager.isChosingTroop = false;
    }

    public void RetractMenu()
    {
        retractMenu.SetActive(!retractMenu.activeSelf);
    }

    public void AddHealer()
    {
        if(capacity-healerCoast>=0)
        {
            capacity -= healerCoast;
            healerCount++;
            healerText.text="x"+healerCount.ToString();
            capacityText.text="Capacity\n\n"+capacity.ToString();
        }
    }
    public void RemoveHealer()
    {
        if (healerCount>0)
        {
            capacity += healerCoast;
            healerCount--;
            healerText.text = "x" + healerCount.ToString(); 
            capacityText.text = "Capacity\n\n" + capacity.ToString();

        }
    }
    public void AddAssassin()
    {
        if (capacity - assassinCoast >= 0)
        {
            capacity -= assassinCoast;
            assassinCount++;
            assassinText.text = "x" + assassinCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();
        }
    }
    public void RemoveAssassin()
    {
        if (assassinCount > 0)
        {
            capacity += assassinCoast;
            assassinCount--;
            assassinText.text = "x" + assassinCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();

        }
    }
    public void AddTank()
    {
        if (capacity - tankCoast >= 0)
        {
            capacity -= tankCoast;
            tankCount++;
            tankText.text = "x" + tankCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();
        }
    }
    public void RemoveTank()
    {
        if (tankCount > 0)
        {
            capacity += tankCoast;
            tankCount--;
            tankText.text = "x" + tankCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();

        }
    }
    public void AddSorcier()
    {
        if (capacity - sorcierCoast >= 0)
        {
            capacity -= sorcierCoast;
            sorcierCount++;
            sorcierText.text = "x" + sorcierCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();
        }
    }
    public void RemoveSorcier()
    {
        if (sorcierCount > 0)
        {
            capacity += sorcierCoast;
            sorcierCount--;
            sorcierText.text = "x" + sorcierCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();

        }
    }
    public void AddSwarmies()
    {
        if (capacity - swarmiesCoast >= 0)
        {
            capacity -= swarmiesCoast;
            swarmiesCount++;
            swarmiesText.text = "x" + swarmiesCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();
        }
    }
    public void RemoveSwarmies()
    {
        if (swarmiesCount > 0)
        {
            capacity += swarmiesCoast;
            swarmiesCount--;
            swarmiesText.text = "x" + swarmiesCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();

        }
    }
    public void AddSniper()
    {
        if (capacity - sniperCoast >= 0)
        {
            capacity -= sniperCoast;
            sniperCount++;
            sniperText.text = "x" + sniperCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();
        }
    }
    public void RemoveSniper()
    {
        if (sniperCount > 0)
        {
            capacity += sniperCoast;
            sniperCount--;
            sniperText.text = "x" + sniperCount.ToString();
            capacityText.text = "Capacity\n\n" + capacity.ToString();

        }
    }

    public void SummonTroops()
    {
        for(int i = 0; i < healerCount; i++) {
            GameManager.SpawnBot(healerPrefab, RandomPosition());
        }
        for (int i = 0; i < assassinCount; i++)
        {
            GameManager.SpawnBot(assassinPrefab, RandomPosition());
        }
        for (int i = 0; i < tankCount; i++)
        {
            GameManager.SpawnBot(tankPrefab, RandomPosition());
        }
        for (int i = 0; i < sorcierCount; i++)
        {
            GameManager.SpawnBot(sorcierPrefab, RandomPosition());
        }
        for (int i = 0; i < swarmiesCount; i++)
        {
            Instantiate(swarmiesPrefab, RandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < sniperCount; i++)
        {
            GameManager.SpawnBot(sniperPrefab, RandomPosition());
        }
        healerCount = 0;
        assassinCount = 0;
        tankCount = 0;
        sorcierCount = 0;
        swarmiesCount = 0;
        sniperCount = 0;
        gameObject.SetActive(false);
    }
    private Vector3 RandomPosition()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomRadius = Random.Range(0f, spawnRadius);
        if(spawnPoint != null )
        {
            return new Vector3(spawnPoint.transform.position.x + randomRadius * Mathf.Cos(randomAngle), spawnPoint.transform.position.y + randomRadius * Mathf.Sin(randomAngle), transform.position.z);
        }

        return new Vector3(randomRadius * Mathf.Cos(randomAngle), 0, randomRadius * Mathf.Sin(randomAngle));


        // Instancier l'unité à la position calculée
    }
}
