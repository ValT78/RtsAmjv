using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] static bool hasFlag = true;

    public static Transform spawnPoint;
    public static Transform crown;

    private void Awake()
    {
        spawnPoint = transform.Find("SpawnPoint");
        crown = transform;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.playerAttack)
        {
            if (other.transform.TryGetComponent(out TroopBot crowned) && !hasFlag)
            {
                crowned.GiveCrown();
                gameObject.SetActive(false);
                hasFlag = false;
                foreach (EnemyBot enemy in GameManager.enemyUnits)
                {
                    enemy.SetKingTarget(crowned);
                }
            }
            
        }
        else
        {
            if (other.transform.TryGetComponent(out EnemyBot crowned) && !hasFlag)
            {
                crowned.GiveCrown();
                gameObject.SetActive(false);
                hasFlag = false;
                foreach (EnemyBot enemy in GameManager.enemyUnits)
                {
                    enemy.SetKingTarget(crowned);
                }
                if (spawnPoint != null)
                {
                    crowned.SetMainTarget(spawnPoint.position);
                }
            }
        }
        
    }
}
