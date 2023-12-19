using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] static bool isForPlayer;
    [SerializeField] static bool hasFlag = true;
    private GameObject crown;

    private void Start()
    {
        crown = transform.Find("Crown").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.TryGetComponent<TroopBot>(out TroopBot player) && !hasFlag) return;
        player.GiveCrown();
        crown.SetActive(false);
        hasFlag = false;
        foreach(EnemyBot enemy in GameManager.enemyUnits)
        {
            enemy.SetKingTarget(player);
        }
    }
}
