using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sorcier : AttackController
{
    [SerializeField] private GameObject freezeParticle;
    [SerializeField] private GameObject freezeCircle;
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
        
        if (GameManager.ClosestBot(!botBase.isEnemy, targetPosition, specialStunnedRadius).TryGetComponent(out BotBase target))
        {
            StartCoroutine(target.StunnedCoroutine(specialStunnedDelay));
            Instantiate(freezeCircle, target.transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity);
            Instantiate(freezeParticle, target.transform.position, Quaternion.Euler(-90, 0, 0));
            return true;
        }
        return false;
    }

}
