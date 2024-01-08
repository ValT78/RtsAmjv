using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : AliveObject
{
    public void Initialize(bool isEnemy)
    {
        this.isEnemy = isEnemy;
        if (isEnemy) GameManager.enemyObjects.Add(this);
        else
        {
            GameManager.troopObjects.Add(this);
        }
    }
}
