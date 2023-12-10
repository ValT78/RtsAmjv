using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : BotBase
{
    
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] protected int damage;

    [Header("Healer")]
    [SerializeField] private float healRadius;
    [SerializeField] private int healAmount;
    [SerializeField] private int boostDamage;
    [SerializeField] private float boostRadius;
    [SerializeField] private float boostDuration;
    private bool isBoosted;
    private float boostTimer;

    public override void UpdateBehavior()
    {
        if(boostTimer> 0)
        {
            print(boostTimer);

            boostTimer -= (Time.deltaTime);
            if(boostTimer <= 0) {

                damage -= boostDamage;
                isBoosted = false;
                print(damage);

            }
        }
    }

    public override IEnumerator Attack(AttackController toShoot)
    {
        // Logique pour attaquer la cible en lançant un projectile.
        GameObject projectile = Instantiate(projectilePrefab, attackOrigin.position, attackOrigin.rotation);
        projectile.GetComponent<Rigidbody>().AddForce((toShoot.transform.position - attackOrigin.position).normalized * projectileSpeed, ForceMode.Impulse);
        if (projectile.TryGetComponent<Projectile>(out var projectileScript))
        {
            projectileScript.Initialize(isEnemy, projectileLifeTime, damage);

        }

        if (fighterType == FighterTypes.Healer) HealerAttack();

        yield return new WaitForSeconds(timeBetweenShot);
        if (toShoot != null) StartCoroutine(Attack(toShoot));
    }

    public override void SpecialAttack()
    {
        if (fighterType == FighterTypes.Healer) HealerSpecial();
    }

    private void HealerAttack()
    {
        foreach(var troop in GameManager.GetCrewBots(isEnemy))
        {
            float distance = Vector3.Distance(transform.position, troop.transform.position);
            if (distance < healRadius && distance!=0 )
            {
                troop.ModifyHealth(healAmount);
            }
        }
    }

    private void HealerSpecial()
    {
        foreach (var troop in GameManager.GetCrewBots(isEnemy))
        {
            float distance = Vector3.Distance(transform.position, troop.transform.position);
            if (distance < boostRadius && distance != 0)
            {
                if(!troop.isBoosted) troop.damage += boostDamage;
                troop.boostTimer = boostDuration;
                troop.isBoosted = true;
            }
        }
       
    }
}
