using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sniper : AttackController
{
    [SerializeField] private float throwSpeed; // La durée totale du lancer
    [SerializeField] private GameObject trap; // Le prefab du projectile à lancer

    public override void StartBehavior()
    {
    }

    public override void SpecialAttack(Vector3 targetPosition)
    {
        GameObject trapPrefab = Instantiate(trap, transform.position, Quaternion.identity);
        trapPrefab.GetComponent<Cloche>().Initialize(targetPosition, throwSpeed, botBase.isEnemy);
    }

    

    
}
