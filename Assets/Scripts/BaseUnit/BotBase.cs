using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotBase : AliveObject
{
    [HideInInspector] public AttackController attackController;
    protected Camera mainCamera;

    [Header("Deplacements")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeComputeDestination;
    private float timerComputeDestination;
    protected NavMeshAgent agent;
    [HideInInspector] public AliveObject target;
    [SerializeField] protected float awareRange;
    [SerializeField] protected bool isAware;
    [SerializeField] protected float voiceRange;
    protected Vector3 mainTarget;

    [Header("Ciblage")]
    public float attackRange;
    [SerializeField] private float initialShotTime;
    private float initialShotTimer;
    protected AliveObject toShoot;
    private bool isAttacking;

    [Header("Miscellaneous")]
    private Transform crown;
    private bool hasCrown;
    private AliveObject kingTarget;


    public override void StartBehavior()
    {
        base.StartBehavior();
        crown = transform.Find("Crown");
        mainCamera = Camera.main;
        initialShotTimer = initialShotTime;
        agent = GetComponent<NavMeshAgent>();
        mainTarget = transform.position;
        attackController = GetComponent<AttackController>();
        agent.speed = moveSpeed;
    }


    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        if (Input.GetKeyDown(KeyCode.Space) && !isEnemy)
        {
            attackController.Special();
        }

        if (toShoot != null)
        {   transform.LookAt(toShoot.transform.position);
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
                    if(isEnemy)
                    {
                        foreach (EnemyBot enemy in GameManager.enemyUnits)
                        {
                            if(Vector3.Distance(transform.position, enemy.transform.position) < voiceRange && enemy.target == null)
                            {
                               enemy.target = target;
                               
                            }
                            
                        }

                    }
                }
                else if (target == null)
                {
                    if (kingTarget != null) PathFind(kingTarget.transform.position);
                    else PathFind(mainTarget);
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
        // Les unités contrôlées par le joueur vise forcément leur target si elle est dans leur range d'attaque
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
            // ON cherche 
            AliveObject newToShoot = ClosestBot(GameManager.GetCrewBots(!isEnemy), attackRange);
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

    public AliveObject GetTarget()
    {
        return target;
    }

    public AliveObject GetToShoot()
    {
        return toShoot;
    }
    public void GiveCrown()
    {
        crown.gameObject.SetActive(true);
        hasCrown = true;
    }
    public void SetKingTarget(AliveObject target)
    {
        kingTarget = target;
    }

}
