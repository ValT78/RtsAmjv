using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

public abstract class BotBase : MonoBehaviour
{
    public bool isEnemy;
    public AttackController attackController;
    protected Camera mainCamera;

    [Header("Vie")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth;
    [SerializeField] private int armor;
    private int health;

    [Header("Deplacements")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeComputeDestination;
    private float timerComputeDestination;
    protected NavMeshAgent agent;
    [HideInInspector] public BotBase target;
    [SerializeField] protected float awareRange;
    protected bool isAware;
    protected Vector3 mainTarget;

    [Header("Ciblage")]
    [SerializeField] private float attackRange;
    [SerializeField] private float initialShotTime;
    private float initialShotTimer;
    protected BotBase toShoot;
    private bool isAttacking;

    [Header("Miscellaneous")]
    private Transform crown;
    private bool hasCrown;

    private void Start()
    {
        crown = transform.Find("Crown");
        mainCamera = Camera.main;
        initialShotTimer = initialShotTime;
        ModifyHealth(maxHealth);
        agent = GetComponent<NavMeshAgent>();
        mainTarget = transform.position;
        StartBehavior();
    }
    public virtual void StartBehavior()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            attackController.Special();
        }

        if (toShoot != null)
        {
            initialShotTimer -= Time.deltaTime;
            // Passer � l'�tat d'attaque si la cible est � port�e.
            if (initialShotTimer < 0 && !isAttacking)
            {
                StartCoroutine(attackController.Basic(toShoot));
                isAttacking = true;
            }
        }
        else
        {
            if (isAware)
            {
                // Rajouter ici : if (isEnemy && y'a un roi sur la map) {target = le roi} else { ce qui est en dessous}
                BotBase newTarget = ClosestBot(GameManager.GetCrewBots(!isEnemy), awareRange);
                if (newTarget != null)
                {
                    target = newTarget;
                }
                else if(target == null)
                {
                    PathFind(mainTarget);
                }
                
            }

            if (target != null)
            {
                PathFind(target.transform.position);
            }
            else
            {
                // Nothing
            }
        }
        FindToShoot();

        UpdateBehavior();
    }

    public virtual void UpdateBehavior()
    {
        
    }

    protected BotBase ClosestBot(List<BotBase> troupList, float maxRange)
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

    private void FindToShoot()
    {
        if (!isEnemy && target!= null && target.isEnemy && Vector3.Distance(target.transform.position, transform.position) <= attackRange)
        {
            if (toShoot != target)
            {
                agent.ResetPath();
                toShoot = target;
                isAttacking = false;
                initialShotTimer = initialShotTime;
            }
        }
        else
        {
            
            BotBase newToShoot = ClosestBot(GameManager.GetCrewBots(!isEnemy), attackRange);
            if (newToShoot == null)
            {
                toShoot = null;
            }
            else if (toShoot == null)
            {
                toShoot = newToShoot;
                isAttacking = false;
                initialShotTimer = initialShotTime;
                if(isEnemy) agent.ResetPath();

            }
            else if (Vector3.Distance(toShoot.transform.position, transform.position) > attackRange)
            {
                toShoot = null;

            }
        }
    }

    protected void PathFind(Vector3 target)
    {
        timerComputeDestination -= Time.deltaTime;
        if(timerComputeDestination < 0)
        {
            agent.SetDestination(target);
            timerComputeDestination = timeComputeDestination;
        }
    }

    
    public void ModifyHealth(int value)
    {
        if (value < 0)
        {
            value = Mathf.Min(armor + value, 0);
        }
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

    public BotBase GetTarget()
    {
        return target;
    }

    public BotBase GetToShoot()
    {
        return toShoot;
    public void giveCrown()
    {
        crown.gameObject.SetActive(true);
    }
}
