using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Assassin : AttackController
{
    [SerializeField] private float throwSpeed; // La durée totale du lancer
    [SerializeField] private GameObject bait; // Le prefab du projectile à lancer


    public override void StartBehavior()
    {
    }

    public override bool SpecialAttack(Vector3 targetPosition)
    {
        GameObject baitPrefab = Instantiate(bait, transform.position, Quaternion.identity);
        baitPrefab.GetComponent<Cloche>().Initialize(targetPosition, throwSpeed, botBase.isEnemy);
        return true;
    }
}
