using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Win Menu")]
    [SerializeField] private GameObject winMenu;
    [SerializeField] private TMP_Text DeadAllieCount;
    [SerializeField] private TMP_Text DeadEnemyCount;

    public void ActiveWinUI()
    {
        winMenu.SetActive(true);
        DeadAllieCount.text = GameManager.deadAllie.ToString();
        DeadEnemyCount.text = GameManager.deadEnemy.ToString();

    }

}
