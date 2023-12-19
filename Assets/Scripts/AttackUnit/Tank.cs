using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : AttackController
{
    [SerializeField] private float boostRadius;
    [SerializeField] private float boostDuration;
    [SerializeField] private int boostArmor;
    private bool isBoostActive;


    public override void StartBehavior()
    {
        base.StartBehavior();
    }

    public override void UpdateBehavior()
    {
        base.UpdateBehavior();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBoostActive && other.TryGetComponent<AliveObject>(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
        {
            obj.SwitchBoostArmor(boostArmor);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isBoostActive && other.TryGetComponent<AliveObject>(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
        {
            obj.SwitchBoostArmor(-boostArmor);

        }
    }

    public override void SpecialAttack(Vector3 target)
    {
        StartCoroutine(BoostTimer());
    }

    private IEnumerator BoostTimer()
    {
        isBoostActive = true;
        foreach (Collider col in Physics.OverlapSphere(transform.position, boostRadius))
        {
            if (col.TryGetComponent<AliveObject>(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
            {
                obj.SwitchBoostArmor(boostArmor);

            }
        }

        yield return new WaitForSeconds(boostDuration);
        isBoostActive = false;

        foreach (Collider col in Physics.OverlapSphere(transform.position, boostRadius))
        {
            if (col.TryGetComponent<AliveObject>(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
            {
                obj.SwitchBoostArmor(-boostArmor);
            }
        }
    }

    
}
