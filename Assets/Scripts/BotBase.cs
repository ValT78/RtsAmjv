using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotBase : MonoBehaviour
{


    [Header("Vie")]
    [SerializeField] private int maxHealth;
    private int health;


    [Header("Déplacements")]
    [HideInInspector] public BotBase target;
    [SerializeField] private float moveSpeed;
    private string CurrentState;

    [Header("Ciblage")]
    [SerializeField] private float AttackRange;
    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float initialShotTime;
    private float initialShotTimer;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private int damage;

    private void Start()
    {
        initialShotTimer = initialShotTime;
    }

    public virtual void UpdateBehavior()
    {
        // Logique commune à tous les bots.
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= AttackRange)
            {
                initialShotTimer -= Time.deltaTime;
                // Passer à l'état d'attaque si la cible est à portée.
                if (initialShotTimer < 0 && CurrentState != "Attack")
                {
                    StartCoroutine(Attack());
                    CurrentState = "Attack";
                }
            }
            else 
            {
                // Avancer vers la cible.
                initialShotTimer = initialShotTime;

                CurrentState = "Move";
                MoveTowardsTarget();
            }
            
        }
        else
        {
            initialShotTimer = initialShotTime;

            CurrentState = "Wait";
        }
    }

   

    protected void MoveTowardsTarget()
    {
        // Calculer la direction vers la cible
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Déplacer l'objet dans la direction de la cible
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    private IEnumerator Attack()
    {
        // Logique pour attaquer la cible en lançant un projectile.
        GameObject projectile = Instantiate(projectilePrefab, attackOrigin.position, attackOrigin.rotation);
        projectile.GetComponent<Rigidbody>().AddForce((target.transform.position-attackOrigin.position).normalized*projectileSpeed, ForceMode.Impulse);
        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
            {
                projectileScript.Initialize(target, projectileLifeTime, damage);

            }
        yield return new WaitForSeconds(timeBetweenShot);
        if(CurrentState=="Attack") StartCoroutine(Attack());
    }

    public void ModifyHealth(int value)
    {
        health = Mathf.Min(health + value, maxHealth);
        if (health < 0)
        {
            GameManager.KillBot(gameObject);
        }
    }
}
