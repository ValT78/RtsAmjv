using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Miscellaneous")]
    private Transform crown;
    private bool hasCrown;
    private BotBase kingTarget;
    private bool isStunned =false;


    public override void StartBehavior()
    {
        base.StartBehavior();
        crown = transform.Find("Crown");
        mainCamera = Camera.main;
        initialShotTimer = initialShotTime;
        agent = GetComponent<NavMeshAgent>();
        mainTarget = transform.position;
    }


    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        if (isStunned) return;
        if (Input.GetKeyDown(KeyCode.Space) && !isEnemy)
        {
            attackController.Special();
        }

        if (toShoot != null)
        {
/*            print("toShoot de " + this.ToString() + " : " + toShoot.ToString());
*/            transform.LookAt(toShoot.transform.position);
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
/*        print("target de " + this.ToString() + " : " + target.ToString());
*/
        FindToShoot();

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
            AliveObject newToShoot = GameManager.ClosestAlive(!isEnemy,transform.position, attackRange);
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

    public void SetTarget(AliveObject target, bool otherSwarm = false)
    {

        if (isSwarm && !otherSwarm)
        {
            isSwarm.SetEveryTarget(target);
        }
        else
        {
            this.target = target;
            isAware = false;
        }

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
    public void SetKingTarget(BotBase target)
    {
        kingTarget = target;
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

}
