using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        if(isEnemy)
        {
            if (other.TryGetComponent<TroopBot>(out var troop))
            {
                troop.ModifyHealth(-damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.TryGetComponent<EnemyBot>(out var enemy))
            {
                enemy.ModifyHealth(-damage);
                Destroy(gameObject);
            }
        }
        // Vérifier la collision avec la cible.
        
    }
}

