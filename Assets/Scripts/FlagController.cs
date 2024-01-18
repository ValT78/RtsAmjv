using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] static bool hasFlag = true;

    public static SpawnPoint spawnPoint;
    public static Transform crown;

    private void Awake()
    {
        spawnPoint = FindObjectOfType<SpawnPoint>();
        crown = transform;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.playerAttack)
        {
            if (other.transform.TryGetComponent(out TroopBot crowned))
            {
                crowned.GiveCrown();
                hasFlag = false;
                foreach (EnemyBot enemy in GameManager.enemyUnits)
                {
                    enemy.SetKingTarget(crowned);
                }
                gameObject.SetActive(false);

            }

        }
        else
        {
            if (other.transform.TryGetComponent(out EnemyBot crowned))
            {

                crowned.GiveCrown();
                hasFlag = false;
                foreach (EnemyBot enemy in GameManager.enemyUnits)
                {
                    enemy.SetKingTarget(crowned);
                }
                if (spawnPoint != null)
                {
                    crowned.SetMainTarget(spawnPoint.transform.position);
                }
                gameObject.SetActive(false);

            }
        }
        
    }
}
