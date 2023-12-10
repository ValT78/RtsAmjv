using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TroopBot : AttackController
{
    
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();

    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = false;
    }

    public void GoToPosition()
    {
        isAware = false;
        target = null;
    }

    public void GoToBot(AttackController target)
    {
        isAware = false;
        this.target = target;
    }

    public void AwarePosition(Vector3 mainTarget)
    {
        isAware = true;
        this.target = null;
        this.mainTarget = mainTarget;  
    }
}
