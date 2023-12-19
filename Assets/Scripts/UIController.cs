using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Win Menu")]
    [SerializeField] private GameObject winMenu;
    [SerializeField] private TMP_Text DeadAllieCount;
    [SerializeField] private TMP_Text DeadEnemyCount;
    [SerializeField] private TMP_Text TimeCount;
    private DateTime start;
    public void Start()
    {
        start = DateTime.Now;
    }

    [ContextMenu("ActiveWinUI")]
    public void ActiveWinUI()
    {
        winMenu.SetActive(true);
        DeadAllieCount.text = GameManager.deadAllie.ToString();
        DeadEnemyCount.text = GameManager.deadEnemy.ToString();
        TimeCount.text = FormatTime(DateTime.Now - start);

    }

    private String FormatTime(TimeSpan time)
    {
        if(time.Hours != 0) return time.Hours + " h " + time.Minutes + " m";
        return time.Minutes + " m " + time.Seconds + " s";
    }

}
