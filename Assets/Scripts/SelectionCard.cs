using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCard : MonoBehaviour
{

    [SerializeField] private GameObject healerPrefab;
    [SerializeField] private GameObject assassinPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject sorcierPrefab;
    [SerializeField] private GameObject swarmiesPrefab;
    [SerializeField] private GameObject sniperPrefab;
    [SerializeField] private TextMeshProUGUI capacityText;
    [SerializeField] private TextMeshProUGUI healerText;
    [SerializeField] private TextMeshProUGUI assassinText;
    [SerializeField] private TextMeshProUGUI tankText;
    [SerializeField] private TextMeshProUGUI sorcierText;
    [SerializeField] private TextMeshProUGUI swarmiesText;
    [SerializeField] private TextMeshProUGUI sniperText; 
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
            GameManager.SpawnBot(healerPrefab, new(0, 2, 0));
        }
        for (int i = 0; i < assassinCount; i++)
        {
            GameManager.SpawnBot(assassinPrefab, new(0, 3, 0));
        }
        for (int i = 0; i < tankCount; i++)
        {
            GameManager.SpawnBot(tankPrefab, new(0, 4, 0));
        }
        for (int i = 0; i < sorcierCount; i++)
        {
            GameManager.SpawnBot(sorcierPrefab, new(0, 5, 0));
        }
        for (int i = 0; i < swarmiesCount; i++)
        {
            Instantiate(swarmiesPrefab);
        }
        for (int i = 0; i < sniperCount; i++)
        {
            GameManager.SpawnBot(sniperPrefab, new(0, 6, 0));
        }
        healerCount = 0;
        assassinCount = 0;
        tankCount = 0;
        sorcierCount = 0;
        swarmiesCount = 0;
        sniperCount = 0;
        gameObject.SetActive(false);
    }
}
