using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TroopBot : BotBase
{
    [SerializeField] private GameObject Selector;
    [SerializeField] private GameObject VisuCapa;
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
    }

    public override void StartBehavior()
    {
        base.StartBehavior();
        isEnemy = false;
    }

    public void GoToPosition(Vector3 position, bool otherSwarm = false)
    {
        if (isSwarm && !otherSwarm) isSwarm.SetEveryPosition(position);
        else
        {
            agent.SetDestination(position);
            isAware = false;
            SetTarget(null);
        }
    }

    public void GoToBot(AliveObject target, bool otherSwarm = false)
    {
        if (isSwarm && !otherSwarm) isSwarm.SetEveryTarget(target);
        else
        {
            isAware = false;
            SetTarget(target);
        }
    }

    public void AwarePosition(Vector3 mainTarget, bool otherSwarm = false)
    {
        if (isSwarm && !otherSwarm) isSwarm.SetEveryAware(mainTarget);
        else
        {
            isAware = true;
            SetTarget(null);
            this.mainTarget = mainTarget;
        }
    }

    public void SelectMode(bool state)
    {
       Selector.SetActive(state);
       
    }

    public void ShowCapa(bool state, bool otherSwarm = false)
    {
        if (isSwarm && !otherSwarm) isSwarm.ShowEveryCapa(state);
        else
        {
            if(!attackController.specialUsed)
                VisuCapa.SetActive(state);
        }
    }
}
