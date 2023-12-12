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

    [Header("CapaSpé")]
    [SerializeField] private float specialRange;
    private bool specialUsed;

    [Header("Healer")]
    [SerializeField] private float healRadius;
    [SerializeField] private int healAmount;
    [SerializeField] private int boostDamage;
    [SerializeField] private float boostRadius;
    [SerializeField] private float boostDuration;
    private bool isBoosted;
    private float boostTimer;

    [Header("Sniper")]
    [SerializeField] private float throwHeight; // La hauteur du lancer en cloche
    [SerializeField] private float throwDuration; // La durée totale du lancer
    [SerializeField] private GameObject trap; // Le prefab du projectile à lancer


    public override void UpdateBehavior()
    {
        if(boostTimer> 0)
        {

            boostTimer -= (Time.deltaTime);
            if(boostTimer <= 0) {

                damage -= boostDamage;
                isBoosted = false;

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
        if (toShoot != null && toShoot == this.toShoot) StartCoroutine(Attack(toShoot));
    }

    public override void SpecialAttack()
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
                if (fighterType == FighterTypes.Healer) HealerSpecial();
                if (fighterType == FighterTypes.Assassin) AssassinSpecial(hit.point);

            }
            
        }
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

    private void AssassinSpecial(Vector3 targetPosition)
    {
        // Instanciez le projectile à la position du joueur (ajustez cela en fonction de votre besoin)
        GameObject trapPrefab = Instantiate(trap, transform.position + new Vector3(0f,3f,0f), Quaternion.identity);
        // Démarrez une coroutine pour gérer le mouvement en cloche du projectile
        StartCoroutine(LaunchProjectileCoroutine(trapPrefab, targetPosition));
    }

    IEnumerator LaunchProjectileCoroutine(GameObject trap, Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < throwDuration*3/4)
        {
            // Calculez la position actuelle du projectile en fonction du temps écoulé
            float t = elapsedTime / throwDuration;
            trap.transform.position = CalculateBezierPoint(t, trap.transform.position, targetPosition, throwHeight);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Lorsque le projectile atteint le sol, instanciez le prefab de l'effet de touche et détruisez le projectile
        InstantiateHitEffect(trap.transform.position);
        Destroy(trap);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, float height)
    {
        // Formule du mouvement en cloche de Bezier
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * (p0 + Vector3.up * height);
        p += 3 * u * tt * (p1 + Vector3.up * height);
        p += ttt * p1;

        return p;
    }

    void InstantiateHitEffect(Vector3 position)
    {
        // Instanciez ici le prefab de l'effet de touche au sol
        // Assurez-vous de configurer correctement cet effet
        // par exemple, une explosion, des particules, etc.
    }
}
