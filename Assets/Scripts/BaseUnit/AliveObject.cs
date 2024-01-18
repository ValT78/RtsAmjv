using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObject : MonoBehaviour
{
    public bool isEnemy;

    [Header("Vie")]
    public BoostUIManager boostManager;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth;
    [SerializeField] private int armor;
    [SerializeField] private int dodgeCount;
    private int health;

    [Header("Tank")]
    [HideInInspector] public int armorBoostCount;



    // Start is called before the first frame update
    void Start()
    {
        StartBehavior();

    }
    public virtual void StartBehavior()
    {
        health = maxHealth;
        if (dodgeCount != 0) boostManager.ActivateBoost(2);
        print("1");
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isChosingTroop) return;
        UpdateBehavior();
    }

    public virtual void UpdateBehavior()
    {

    }

    public void ModifyHealth(int value)
    {

        if (value < 0)
        {
            if (dodgeCount > 0)
            {
                dodgeCount--;
                value = 0;
                if(dodgeCount == 0) boostManager.ActivateBoost(2);
            }
            else
            {
                value = Mathf.Min(armor + value, 0);
            }
        }
        health = Mathf.Min(health + value, maxHealth);
        

        if (value <= 0)
        {
            healthBar.DamageHealthBar(health, maxHealth);
        }
        else
        {
            healthBar.HealHealthBar(health, maxHealth);
        }

        if (health <= 0)
        {
            GameManager.KillBot(gameObject);
        }
    }

    public void SwitchBoostArmor(int value)
    {
        if(value > 0) {
            if (armorBoostCount == 0)
            {
                ModifyArmor(value);
                boostManager.ActivateBoost(0);
            }
            armorBoostCount++;
        }
        else
        {
            armorBoostCount--;
            if (armorBoostCount == 0)
            {
                ModifyArmor(value);
                boostManager.ActivateBoost(0);
            }
        }

    }

    public void ModifyArmor(int value)
    {
        armor += value;
    }
}
