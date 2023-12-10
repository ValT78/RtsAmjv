using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BotBase : MonoBehaviour
{
    protected bool isEnemy;
    public FighterTypes fighterType;

    [Header("Vie")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Deplacements")]
    [SerializeField] private float moveSpeed;
    [HideInInspector] public AttackController target;

    [Header("Ciblage")]
    [SerializeField] private float AttackRange;
    [SerializeField] private float initialShotTime;
    [SerializeField] protected float timeBetweenShot;
    private float initialShotTimer;
    private AttackController toShoot;
    private bool isAttacking;


    public enum FighterTypes
    {
        Assassin,
        Healer,
        Tank,
        Swarmies,
        Sniper,
        Sorcier

    }
    public virtual IEnumerator Attack(AttackController toShoot)
    {
        yield return null;
    }
    public virtual void SpecialAttack()
    {
        return;
    }
    private void Start()
    {
        initialShotTimer = initialShotTime;
        ModifyHealth(maxHealth);
        StartBehavior();
    }
    public virtual void StartBehavior()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpecialAttack();
        }
        if (toShoot != null)
        {
            initialShotTimer -= Time.deltaTime;
            // Passer à l'état d'attaque si la cible est à portée.
            if (initialShotTimer < 0 && !isAttacking)
            {
                StartCoroutine(Attack(toShoot));
                isAttacking = true;
            }
        }
        else
        {
            isAttacking = false;
            initialShotTimer = initialShotTime;
            AttackController newToShoot = ClosestBot(GameManager.GetCrewBots(!isEnemy), AttackRange);

            if (newToShoot != null) toShoot = newToShoot;

            else
            {
                if (target != null)
                {
                    MoveTowardsTarget();
                }
                else
                {
                    // Waiting
                }
            }

        }
        UpdateBehavior();
    }

    public virtual void UpdateBehavior()
    {
        
    }

    protected AttackController ClosestBot(List<AttackController> troupList, float maxRange)
    {
        if (troupList.Count == 0) return null;
        float cloasestRange = maxRange;
        AttackController closestBot = null;
        // Logique pour détecter les troupes dans la visionRange.
        foreach (var troop in troupList)
        {
            float distanceToTroop = Vector3.Distance(transform.position, troop.transform.position);
            if (distanceToTroop <= cloasestRange)
            {
                // La troupe détectée devient la nouvelle cible.
                closestBot = troop;
                cloasestRange = distanceToTroop;
            }
        }

        return closestBot;
    }

    protected void MoveTowardsTarget()
    {
        // Calculer la direction vers la cible
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Déplacer l'objet dans la direction de la cible
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    
    public void ModifyHealth(int value)
    {
        health = Mathf.Min(health + value, maxHealth);
        if (value <= 0) {
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
}
