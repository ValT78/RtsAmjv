using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(GameManager.playerAttack)
        {
            if(other.TryGetComponent(out TroopBot bot) && bot.GetHasCrown())
            {
                GameManager.SetVictoryScreen(true);
            }
        }
        else
        {
            if (other.TryGetComponent(out EnemyBot bot) && bot.GetHasCrown())
            {
                GameManager.SetVictoryScreen(false);

            }
        }
    }
}
