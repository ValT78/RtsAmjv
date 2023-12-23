using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sorcier : AttackController
{
    [SerializeField] private float specialStunnedDelay;
    public override void StartBehavior()
    {
        
    }

    public override void UpdateBehavior()
    {
        
    }

    public override void SpecialAttack(Vector3 targetPosition)
    {

        StartCoroutine(GameManager.ClosestBot(!botBase.isEnemy, targetPosition, specialRange).StunnedCoroutine(specialStunnedDelay));
    }

}
