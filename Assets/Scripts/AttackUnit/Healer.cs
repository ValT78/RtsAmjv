using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : AttackController
{
    [SerializeField] private GameObject healParticle;
    [SerializeField] private GameObject healCircle;
    [SerializeField] private float healRadius;
    [SerializeField] private int healAmount;
    [SerializeField] private GameObject boostParticle;
    [SerializeField] private GameObject boostCircle;
    [SerializeField] private int boostDamage;
    [SerializeField] private float boostRadius;
    [SerializeField] private float boostDuration;
    

    public override void BasicAttack()
    {
        Instantiate(healCircle, transform.position + new Vector3(0f,-0.5f,0f), Quaternion.identity);
        foreach (var troop in GameManager.GetCrewAlive(botBase.isEnemy))
        {
            float distance = Vector3.Distance(transform.position, troop.transform.position);
            if (distance < healRadius && distance != 0)
            {
                Instantiate(healParticle, troop.transform.position, Quaternion.Euler(-90, 0, 0));
                troop.ModifyHealth(healAmount);
            }
        }
    }

    public override bool SpecialAttack(Vector3 targetPosition)
    {
        bool flag = false;
        foreach (var troop in GameManager.GetCrewBot(botBase.isEnemy))
        {
            float distance = Vector3.Distance(transform.position, troop.transform.position);
            if (distance < boostRadius && distance != 0)
            {
                if(troop.TryGetComponent(out BotBase var))
                {
                    StartCoroutine(var.attackController.HealerBoost(boostDamage, boostDuration));
                    flag = true;
                }
            }
        }
        if(flag)
        {
            Instantiate(boostCircle, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity);
            Instantiate(boostParticle, transform.position, Quaternion.Euler(-90, 0, 0));
        }
        return flag;

    }
}
