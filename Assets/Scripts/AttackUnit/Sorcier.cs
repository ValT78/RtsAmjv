using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sorcier : AttackController
{
    [SerializeField] private GameObject freezeParticle;
    [SerializeField] private GameObject freezeCircle;
    [SerializeField] private float specialStunnedDelay;
    [SerializeField] private float freezeRadius;
    public override void StartBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        
    }

    public override bool SpecialAttack(Vector3 targetPosition)
    {
        bool flag=false;
        foreach(Collider bot in Physics.OverlapSphere(targetPosition, freezeRadius))
        {
            if(bot.TryGetComponent(out BotBase botB) && botB.isEnemy != botBase.isEnemy)
            {
                StartCoroutine(botB.StunnedCoroutine(specialStunnedDelay));
                Instantiate(freezeParticle, botB.transform.position, Quaternion.Euler(-90, 0, 0));
                flag = true;
            }
        }
        if(flag) Instantiate(freezeCircle, targetPosition, Quaternion.identity);


        return flag;
    }

}
