using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] bool isForPlayer;
    [SerializeField] bool hasFlag = true;
    private GameObject crown;

    private void Start()
    {
        crown = transform.Find("Crown").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        TroopBot player;
        if (!other.transform.TryGetComponent<TroopBot>(out player) && hasFlag) return;
        player.giveCrown();
        crown.SetActive(false);
        hasFlag = false;
    }
}
