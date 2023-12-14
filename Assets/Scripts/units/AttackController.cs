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
    [SerializeField] private float projectileLifeTime;
    [SerializeField] protected int damage;

    [Header("CapaSpé")]
    [SerializeField] private float specialRange;
    private bool specialUsed;

    [Header("Healer")]
    private int boostNumber;

    

    public virtual void BasicAttack() { }

    public virtual void SpecialAttack(Vector3 targetPosition) { }

    public void Start()
    {
        
    }

    public void Update()
    {
        
        UpdateBehavior();
    }

    public virtual void UpdateBehavior()
    {
        
    }

    public IEnumerator Basic(BotBase toShoot)
    {
        // Logique pour attaquer la cible en lançant un projectile.
        GameObject projectile = Instantiate(projectilePrefab, attackOrigin.position, attackOrigin.rotation);
        projectile.GetComponent<Rigidbody>().AddForce((toShoot.transform.position - attackOrigin.position).normalized * projectileSpeed, ForceMode.Impulse);
        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
        {
            projectileScript.Initialize(botBase.isEnemy, projectileLifeTime, damage);

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
            specialUsed = true;
            // Vérifiez si le rayon touche un objet
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && Vector3.Distance(transform.position, hit.point) < specialRange)
            {
                SpecialAttack(hit.point);

            }
            
        }
    }

    public IEnumerator HealerBoost(int damageBoost, float boostDuration)
    {
        if(boostNumber==0) ModifyDamage(damageBoost);
        boostNumber++;
        
        yield return new WaitForSeconds(boostDuration);
        boostNumber--;
        if(boostNumber==0) ModifyDamage(-damageBoost);

    }

    public int ModifyDamage(int value)
    {
        damage+= value;
        return damage;
    }

   

    
}
