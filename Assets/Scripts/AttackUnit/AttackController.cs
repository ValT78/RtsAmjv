using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public BotBase botBase;
    private Camera mainCamera;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] protected float timeBetweenShot;
    [SerializeField] private float projectileSpeed;
    [SerializeField] protected int damage;

    [Header("CapaSp�")]
    [SerializeField] private float specialRange;
    private bool specialUsed;

    [Header("Healer")]
    private int damageBoostNumber;

    


    public virtual void BasicAttack() { }

    public virtual void SpecialAttack(Vector3 targetPosition) { }

    public void Start()
    {
        mainCamera = Camera.main;
        StartBehavior();
    }

    public virtual void StartBehavior()
    {

    }
    public void Update()
    {
        
        UpdateBehavior();
    }

    public virtual void UpdateBehavior()
    {
        
    }

    public IEnumerator Basic(AliveObject toShoot)
    {
        // Logique pour attaquer la cible en lan�ant un projectile.
        GameObject projectile = Instantiate(projectilePrefab, attackOrigin.position, transform.rotation);
        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
        {
            projectileScript.Initialize(botBase.isEnemy, botBase.attackRange, projectileSpeed, damage);
        }
        else if (projectile.TryGetComponent<Cloche>(out var clocheScript))
        {
            clocheScript.Initialize(toShoot.transform.position, projectileSpeed, botBase.isEnemy);
        }

        BasicAttack();

        yield return new WaitForSeconds(timeBetweenShot);
        if (toShoot != null && toShoot == botBase.GetToShoot()) StartCoroutine(Basic(toShoot));
    }

    public void Special()
    {
        if(specialUsed)
        {
            // Nothing
        }
        else
        {
            //specialUsed = true;
            // V�rifiez si le rayon touche un objet
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && Vector3.Distance(transform.position, hit.point) < specialRange)
            {
                SpecialAttack(hit.point);

            }
            
        }
    }

    public IEnumerator HealerBoost(int damageBoost, float boostDuration)
    {
        if(damageBoostNumber==0) ModifyDamage(damageBoost);
        damageBoostNumber++;
        
        yield return new WaitForSeconds(boostDuration);
        damageBoostNumber--;
        if(damageBoostNumber==0) ModifyDamage(-damageBoost);

    }

    public int ModifyDamage(int value)
    {
        damage+= value;
        return damage;
    }

   

    
}
