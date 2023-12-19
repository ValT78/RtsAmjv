using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : AttackController
{
    [SerializeField] private float healRadius;
    [SerializeField] private int healAmount;
    [SerializeField] private int boostDamage;
    [SerializeField] private float boostRadius;
    [SerializeField] private float boostDuration;
    

    public override void BasicAttack()
    {
        foreach (var troop in GameManager.GetCrewBots(botBase.isEnemy))
        {
            float distance = Vector3.Distance(transform.position, troop.transform.position);
            if (distance < healRadius && distance != 0)
            {
                troop.ModifyHealth(healAmount);
            }
        }
    }

    public override void SpecialAttack(Vector3 targetPosition)
    {
        foreach (var troop in GameManager.GetCrewBots(botBase.isEnemy))
        {
            float distance = Vector3.Distance(transform.position, troop.transform.position);
            if (distance < boostRadius && distance != 0)
            {
                StartCoroutine(troop.attackController.HealerBoost(boostDamage, boostDuration));
            }
        }

    }
}
