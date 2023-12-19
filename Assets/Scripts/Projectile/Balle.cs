using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool isEnemy;
    private float lifeTime;
    private int damage;

    public void Initialize(bool isEnemy, float lifeTime, int damage)
    {
        this.isEnemy = isEnemy;
        this.lifeTime = lifeTime;
        this.damage = damage;
    }

    void Update()
    {
       // Vérifier la distance parcourue.
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
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

