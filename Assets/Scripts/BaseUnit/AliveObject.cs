using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObject : MonoBehaviour
{
    public bool isEnemy;

    [Header("Vie")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth;
    [SerializeField] private int armor;
    private int health;

    [Header("Tank")]
    [HideInInspector] public bool isArmorBoosted;



    // Start is called before the first frame update
    void Start()
    {
        ModifyHealth(maxHealth);
        StartBehavior();

    }
    public virtual void StartBehavior()
    {

    }
    // Update is called once per frame
    void Update()
    {
        UpdateBehavior();
    }

    public virtual void UpdateBehavior()
    {

    }

    public void ModifyHealth(int value)
    {
        if (value < 0)
        {
            value = Mathf.Min(armor + value, 0);

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

    public void ModifyArmor(int value)
    {
        armor += value;
    }
}
