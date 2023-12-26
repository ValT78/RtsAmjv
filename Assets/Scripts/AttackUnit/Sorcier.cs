using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sorcier : AttackController
{
    [SerializeField] private float specialStunnedDelay;
    [SerializeField] private float specialStunnedRadius;
    public override void StartBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        
    }

    public override bool SpecialAttack(Vector3 targetPosition)
    {
        if (GameManager.ClosestBot(!botBase.isEnemy, targetPosition, specialStunnedRadius).TryGetComponent(out BotBase var))
        {
            StartCoroutine(var.StunnedCoroutine(specialStunnedDelay));
            return true;
        }
        return false;
    }

}
