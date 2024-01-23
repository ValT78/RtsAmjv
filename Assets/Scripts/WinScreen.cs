using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI ally;
    [SerializeField] private TextMeshProUGUI enemy;
    [SerializeField] private TextMeshProUGUI timer;
    public void SetWinner(bool playerWin, int ally, int enemy, float timeElapsed)
    {
        if (playerWin) winText.text = "YOU'VE WIN !!!";
        else winText.text = "YOU LOSE !!!";
        this.ally.text = ally.ToString();
        this.enemy.text = enemy.ToString();
        this.timer.text = Mathf.Round(timeElapsed/60f).ToString()+"m "+ Mathf.Round(timeElapsed % 60f).ToString()+"s";
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

}
