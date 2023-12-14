using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

public abstract class BotBase : MonoBehaviour
{
    protected bool isEnemy;
    public FighterTypes fighterType;
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
    [HideInInspector] public AttackController target;
    [SerializeField] protected float awareRange;
    protected bool isAware;
    protected Vector3 mainTarget;

    [Header("Ciblage")]
    [SerializeField] private float attackRange;
    [SerializeField] private float initialShotTime;
    [SerializeField] protected float timeBetweenShot;
    private float initialShotTimer;
    protected AttackController toShoot;
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
            if (isAware)
            {
                // Rajouter ici : if (isEnemy && y'a un roi sur la map) {target = le roi} else { ce qui est en dessous}
                AttackController newTarget = ClosestBot(GameManager.GetCrewBots(!isEnemy), awareRange);
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
            
            AttackController newToShoot = ClosestBot(GameManager.GetCrewBots(!isEnemy), attackRange);
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
}
