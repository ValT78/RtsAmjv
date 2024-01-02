using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : AttackController
{
    [SerializeField] private GameObject boostParticle;
    [SerializeField] private GameObject boostCircle;
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
        if (isBoostActive && other.TryGetComponent(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
        {
            obj.SwitchBoostArmor(boostArmor);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isBoostActive && other.TryGetComponent(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
        {
            obj.SwitchBoostArmor(-boostArmor);

        }
    }

    public override bool SpecialAttack(Vector3 target)
    {
        StartCoroutine(BoostTimer());
        Instantiate(boostCircle, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity);
        Instantiate(boostParticle, transform.position, Quaternion.Euler(-90, 0, 0));
        return true;
    }

    private IEnumerator BoostTimer()
    {
        isBoostActive = true;
        foreach (Collider col in Physics.OverlapSphere(transform.position, boostRadius))
        {
            if (col.TryGetComponent(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
            {
                obj.SwitchBoostArmor(boostArmor);

            }
        }

        yield return new WaitForSeconds(boostDuration);
        isBoostActive = false;

        foreach (Collider col in Physics.OverlapSphere(transform.position, boostRadius))
        {
            if (col.TryGetComponent(out AliveObject obj) && obj.isEnemy == botBase.isEnemy)
            {
                obj.SwitchBoostArmor(-boostArmor);
            }
        }
    }

    
}
