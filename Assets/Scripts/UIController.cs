using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

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

    public void Start()
    {
        start = DateTime.Now;

        PlayButton.onClick.AddListener(StartAction);
        SettingsButton.onClick.AddListener(ActiveSettingsUI);
        QuitButton.onClick.AddListener(QuitAction);

        BackButton.onClick.AddListener(ActiveMainUI);
        ApplyButton.onClick.AddListener(SetSettings);
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
        MainMenu.SetActive(true);
    }

    public void StartAction()
    {

    }

    public void QuitAction()
    {

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

    private void BackAction()
    {

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

}
