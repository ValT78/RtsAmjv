using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BotBase : AliveObject
{
    public AttackController attackController;
    protected Camera mainCamera;

    [Header("Deplacements")]
    [SerializeField] private float timeComputeDestination;
    private float timerComputeDestination;
    protected NavMeshAgent agent;
    private AliveObject target;
    [SerializeField] protected float awareRange;
    [SerializeField] protected bool isAware;
    [SerializeField] protected float voiceRange;
    protected Swarmies isSwarm;
    protected Vector3 mainTarget;

    [Header("Ciblage")]
    public float attackRange;
    [SerializeField] private float initialShotTime;
    private float initialShotTimer;
    protected AliveObject toShoot;
    private bool isAttacking;
    [SerializeField] private LayerMask wallLayer;

    [Header("Miscellaneous")]
    [SerializeField] private GameObject crown;
    private bool hasCrown;
    private BotBase kingTarget;
    private bool isStunned =false;
    [SerializeField] private float crownSpeedMultiplier;
    [SerializeField] private GameObject Selector;


    public override void StartBehavior()
    {
        base.StartBehavior();
        mainCamera = Camera.main;
        initialShotTimer = initialShotTime;
        agent = GetComponent<NavMeshAgent>();
        
    }

    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        if (isStunned) return;
        
        if(isEnemy && hasCrown)
        {
            PathFind(mainTarget);
            return;
        }

        if (toShoot != null)
        {
            transform.LookAt(toShoot.transform.position);
            initialShotTimer -= Time.deltaTime;
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
                AliveObject newTarget = GameManager.ClosestAlive(!isEnemy, transform.position, awareRange);
                if (newTarget != null)
                {
                    SetTarget(newTarget);

                    if(isEnemy)
                    {
                        foreach (EnemyBot enemy in GameManager.enemyUnits)
                        {
                            if(Vector3.Distance(transform.position, enemy.transform.position) < voiceRange && enemy.GetTarget() == null)
                            {
                               enemy.SetTarget(target);
                               
                            }
                            
                        }

                    }
                }
                else if (target == null)
                {
                    if(kingTarget != null) PathFind(kingTarget.transform.position + (kingTarget.transform.position-transform.position).normalized*5);

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

    
    private void FindToShoot()
    {
        
        // Les unités contrôlées par le joueur vise forcément leur target si elle est dans leur range d'attaque
        if (!isEnemy && target!= null && target.isEnemy && Vector3.Distance(target.transform.position, transform.position) <= attackRange && !WallInRange(target))
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
            AliveObject newToShoot = GameManager.ClosestAlive(!isEnemy,transform.position, attackRange);
           
            if (newToShoot == null)
            {
                toShoot = null;
            }
            else if (toShoot == null)
            {
                if(!WallInRange(newToShoot))
                {
                    toShoot = newToShoot;
                    isAttacking = false;
                    initialShotTimer = initialShotTime;
                    if (isEnemy) agent.ResetPath();
                }
                

            }
            else if (Vector3.Distance(toShoot.transform.position, transform.position) > attackRange || WallInRange(toShoot))
            {
                toShoot = null;

            }
        }

        bool WallInRange(AliveObject target)
        {
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out _, attackRange, wallLayer))
            {
                return true;
            }

            return false;
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

    public void SetTarget(AliveObject target, bool otherSwarm = false)
    {

        if (isSwarm && !otherSwarm)
        {
            isSwarm.SetEveryTarget(target);
        }
        else
        {
            if (this.target != null && this.target.GetType() == typeof(EnemyBot))
            {
                ((EnemyBot)this.target).SelectMode(false);
            }
            this.target = target;
            if (this.target != null && this.target.GetType() == typeof(EnemyBot))
            {
                ((EnemyBot)this.target).SelectMode(true);
            }
        }

    }

    public AliveObject GetToShoot()
    {
        return toShoot;
    }
    public void GiveCrown()
    {
        crown.SetActive(true);
        hasCrown = true;
        agent.speed *= crownSpeedMultiplier;
    }
    public void SetKingTarget(BotBase target)
    {
        kingTarget = target;
    }
    public void SetMainTarget(Vector3 target)
    {
        mainTarget = target;
    }

    public IEnumerator StunnedCoroutine(float delay)
    {
        isStunned = true;
        boostManager.ActivateBoost(3);
        agent.ResetPath();        
        yield return new WaitForSeconds(delay);
        isStunned = false;
        boostManager.ActivateBoost(3);
    }

    public void SetIsSwarm(Swarmies swarmies)
    {
        isSwarm = swarmies;
    }
    public Swarmies GetIsSwarm()
    {
        return isSwarm;
    }

    public bool GetHasCrown()
    {
        return hasCrown;
    }
    public void SelectMode(bool state)
    {
        Selector.SetActive(state);
    }
}
