using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(BotBase))]
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
    [SerializeField] private GameObject star;
    [SerializeField] protected float specialRange;
    [HideInInspector] public bool specialUsed;

    [Header("Healer")]
    private int damageBoostNumber;

    [Header("Sound")]
    private SoundManager Sm;

    


    public virtual void BasicAttack() { }

    public virtual bool SpecialAttack(Vector3 targetPosition) { return true; }

    public void Awake()
    {
        if (!botBase.isEnemy) star.SetActive(true);
    }

    public void Start()
    {
        mainCamera = Camera.main;
        Sm = GetComponent<SoundManager>();
        StartBehavior();
    }

    public virtual void StartBehavior()
    {

    }
    public void Update()
    {
        if (GameManager.isChosingTroop) return;
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

        Sm.PlaySound(SoundManager.SoundType.fire);
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
            // V�rifiez si le rayon touche un objet
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
            if (!hit.IsUnityNull() && Vector3.Distance(transform.position, hit.point) < specialRange)
            {
                if (SpecialAttack(hit.point))
                {
                    specialUsed = true;
                    star.SetActive(false);
                }
            }
            
        }
    }

    public IEnumerator HealerBoost(int damageBoost, float boostDuration)
    {
        if (damageBoostNumber == 0)
        {
            ModifyDamage(damageBoost);
            botBase.boostManager.ActivateBoost(1);
        }
        damageBoostNumber++;
        
        yield return new WaitForSeconds(boostDuration);
        damageBoostNumber--;
        if (damageBoostNumber == 0)
        {
            ModifyDamage(-damageBoost);
            botBase.boostManager.ActivateBoost(1);
        }

    }

    public int ModifyDamage(int value)
    {
        damage+= value;
        return damage;
    }

   public void SetSpecialUsed(bool specialUsed)
    {
        this.specialUsed = specialUsed;
        star.SetActive(!specialUsed);

    }

}
