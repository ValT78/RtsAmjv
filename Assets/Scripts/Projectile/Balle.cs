using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float aliveTime;
    private bool isEnemy;
    private float distance;
    private float bulletSpeed;
    private int damage;

    public void Initialize(bool isEnemy, float distance, float bulletSpeed, int damage)
    {

        this.isEnemy = isEnemy;
        this.distance = distance;
        this.bulletSpeed = bulletSpeed;
        this.damage = damage;
    }

    void Update()
    {
       // Vérifier la distance parcourue.
        distance -= Time.deltaTime*bulletSpeed;
        transform.Translate(bulletSpeed * Time.deltaTime * Vector3.forward);
        if (distance <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out AliveObject alive))
        {
            if(this.isEnemy != alive.isEnemy) {
                alive.ModifyHealth(-damage);
                Destroy(gameObject, aliveTime);
            }
        }
        
        
    }
}

