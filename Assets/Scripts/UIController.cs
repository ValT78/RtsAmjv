using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Win Menu")]
    [SerializeField] private GameObject winMenu;
    [SerializeField] private TMP_Text DeadAllieCount;
    [SerializeField] private TMP_Text DeadEnemyCount;
    [SerializeField] private TMP_Text TimeCount;
    private DateTime start;

    [Header("Main Menu")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button QuitButton;


    [Header("Settings Menu")]
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider GeneralSlider;
    [SerializeField] private Slider EffectSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private TMP_Dropdown ResolutionDrop;
    [SerializeField] private TMP_Dropdown QualityDrop;
    [SerializeField] private Toggle Toggle;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button ApplyButton;
    private Resolution[] resolutions;


    [Header("Level selection")]
    [SerializeField] private GameObject LevelMenu;
    [SerializeField] private Button Lvl1AttackButton;
    [SerializeField] private Button Lvl1DefenceButton;
    [SerializeField] private Button Lvl2AttackButton;
    [SerializeField] private Button Lvl2DefenceButton;
    [SerializeField] private Button BackButton2;


    public void Start()
    {
        start = DateTime.Now;

        PlayButton.onClick.AddListener(StartAction);
        SettingsButton.onClick.AddListener(ActiveSettingsUI);
        QuitButton.onClick.AddListener(QuitAction);

        BackButton.onClick.AddListener(ActiveMainUI);
        ApplyButton.onClick.AddListener(SetSettings);

        Lvl1AttackButton.onClick.AddListener(Attack1Action);
        Lvl1DefenceButton.onClick.AddListener(Defence1Action);
        Lvl2AttackButton.onClick.AddListener(Attack2Action);
        Lvl2DefenceButton.onClick.AddListener(Defence2Action);
        BackButton2.onClick.AddListener(ActiveMainUI);
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

    [ContextMenu("ActiveMainsUI")]
    public void ActiveMainUI()
    {
        SettingsMenu.SetActive(false);
        LevelMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void StartAction()
    {
        SettingsMenu.SetActive(false);
        LevelMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void QuitAction()
    {
        Application.Quit();
    }

    [ContextMenu("ActiveSettingsUI")]
    public void ActiveSettingsUI()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);

        resolutions = Screen.resolutions;

        ResolutionDrop.ClearOptions();
        List<String> options = new List<string>();
        int currentRes = 0;
        for(var i =0; i<resolutions.Length;i++)
        {
            Resolution res = resolutions[i];
            options.Add(res.width + " x " + res.height);
            if(res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        ResolutionDrop.AddOptions(options);
        ResolutionDrop.value = currentRes;
        ResolutionDrop.RefreshShownValue();
    }

    [ContextMenu("SetSettings")]
    private void SetSettings()
    {
        mixer.SetFloat("MainVolume", GeneralSlider.value);
        mixer.SetFloat("EffectVolume", EffectSlider.value);
        mixer.SetFloat("MusicVolume", MusicSlider.value);
        QualitySettings.SetQualityLevel(QualityDrop.value);
        Screen.fullScreen = Toggle.isOn;
    }

    private void Attack1Action()
    {
        SceneManager.LoadScene("LVL 1");
    }

    private void Defence1Action()
    {
        SceneManager.LoadScene("LVL 3");
    }
    private void Attack2Action()
    {
        SceneManager.LoadScene("LVL 2");
    }

    private void Defence2Action()
    {
        SceneManager.LoadScene("LVL 4");
    }

}
