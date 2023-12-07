using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotBase : MonoBehaviour
{
    [SerializeField] private bool isEnemy;

    [Header("Vie")]
    [SerializeField] private int maxHealth;
    private int health;


    [Header("D�placements")]
    [HideInInspector] public BotBase target;
    [SerializeField] private float moveSpeed;
    private string CurrentState;

    [Header("Ciblage")]
    [SerializeField] private float AttackRange;
    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float initialShotTime;
    private float initialShotTimer;
    private BotBase toShoot;
    private bool isAttacking;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private int damage;

    private void Start()
    {
        initialShotTimer = initialShotTime;
        health = maxHealth;
    }

    private void Update()
    {
        UpdateBehavior();
    }

    public virtual void UpdateBehavior()
    {
        if(toShoot != null)
        {
            initialShotTimer -= Time.deltaTime;
            // Passer � l'�tat d'attaque si la cible est � port�e.
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
            BotBase newToShoot = ClosestBot(GameManager.GetOppositeBots(isEnemy), AttackRange);

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

        
    }

    public BotBase ClosestBot(List<BotBase> troupList, float maxRange)
    {
        if (troupList.Count == 0) return null;
        float cloasestRange = maxRange;
        BotBase closestBot = null;
        // Logique pour d�tecter les troupes dans la visionRange.
        foreach (var troop in troupList)
        {
            float distanceToTroop = Vector3.Distance(transform.position, troop.transform.position);
            if (distanceToTroop <= cloasestRange)
            {
                // La troupe d�tect�e devient la nouvelle cible.
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

        // D�placer l'objet dans la direction de la cible
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    private IEnumerator Attack(BotBase toShoot)
    {
        // Logique pour attaquer la cible en lan�ant un projectile.
        GameObject projectile = Instantiate(projectilePrefab, attackOrigin.position, attackOrigin.rotation);
        projectile.GetComponent<Rigidbody>().AddForce((toShoot.transform.position-attackOrigin.position).normalized*projectileSpeed, ForceMode.Impulse);
        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
            {
                projectileScript.Initialize(isEnemy, projectileLifeTime, damage);

            }
        yield return new WaitForSeconds(timeBetweenShot);
        if(toShoot!=null) StartCoroutine(Attack(toShoot));
    }

    public void ModifyHealth(int value)
    {
        health = Mathf.Min(health + value, maxHealth);
        if (health <= 0)
        {
            GameManager.KillBot(gameObject);
        }
    }
}
