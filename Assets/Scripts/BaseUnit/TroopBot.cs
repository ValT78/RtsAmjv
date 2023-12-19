using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TroopBot : BotBase
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

    public void GoToPosition(Vector3 position)
    {
        agent.SetDestination(position);
        isAware = false;
        target = null;
    }

    public void GoToBot(BotBase target)
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
