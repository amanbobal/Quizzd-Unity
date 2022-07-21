using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;

    public GameObject RestartPanel;

    public List<Toggle> ToggleBtns;

    public List<Image> Bg;

    public List<TextMeshProUGUI> Times;

    void Awake()
    {
        if (RestartPanel.activeSelf)
        {
            RestartPanel.SetActive(false);
        }
        else
        {
            return;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("TimerLimit") != true)
        {
            PlayerPrefs.SetFloat("TimerLimit", 30f);
        }
        switch (PlayerPrefs.GetFloat("TimerLimit"))
        {
            case 15f:
                OnLow(true);
                Bg[0].DOColor(Color.white, 0.4f);
                Times[0].DOColor(Color.black, 0.4f);
                Bg[1].DOColor(Color.black, 0.4f);
                Times[1].DOColor(Color.grey, 0.4f);
                Bg[2].DOColor(Color.black, 0.4f);
                Times[2].DOColor(Color.grey, 0.4f);
                break;
            case 30f:
                OnMed(true);
                Bg[0].DOColor(Color.black, 0.4f);
                Times[0].DOColor(Color.grey, 0.4f);
                Bg[1].DOColor(Color.white, 0.4f);
                Times[1].DOColor(Color.black, 0.4f);
                Bg[2].DOColor(Color.black, 0.4f);
                Times[2].DOColor(Color.grey, 0.4f);
                break;
            case 60f:
                OnHigh(true);
                Bg[0].DOColor(Color.black, 0.4f);
                Times[0].DOColor(Color.grey, 0.4f);
                Bg[1].DOColor(Color.black, 0.4f);
                Times[1].DOColor(Color.grey, 0.4f);
                Bg[2].DOColor(Color.white, 0.4f);
                Times[2].DOColor(Color.black, 0.4f);
                break;
        }
    }

    void Update()
    {
        switch (PlayerPrefs.GetFloat("TimerLimit"))
        {
            case 15f:
                ToggleBtns[0].isOn = true;
                break;
            case 30f:
                ToggleBtns[1].isOn = true;
                break;
            case 60f:
                ToggleBtns[2].isOn = true;
                break;
        }
    }

    public void ClearCache()
    {
        PlayerPrefs.DeleteAll();

        RestartPanel.SetActive(true);
    }

    public void Restart()
    {
        Application.Quit();
    }

    public void OnLow(bool enabled)
    {
        if (enabled)
        {
            PlayerPrefs.SetFloat("TimerLimit", 15f);
            Bg[0].DOColor(Color.white, 0.4f);
            Times[0].DOColor(Color.black, 0.4f);
        }
        else
        {
            Bg[0].DOColor(Color.black, 0.4f);
            Times[0].DOColor(Color.grey, 0.4f);
        }
    }
    public void OnMed(bool enabled)
    {
        if (enabled)
        {
            PlayerPrefs.SetFloat("TimerLimit", 30f);
            Bg[1].DOColor(Color.white, 0.4f);
            Times[1].DOColor(Color.black, 0.4f);
        }
        else
        {
            Bg[1].DOColor(Color.black, 0.4f);
            Times[1].DOColor(Color.grey, 0.4f);
        }
    }
    public void OnHigh(bool enabled)
    {
        if (enabled)
        {
            PlayerPrefs.SetFloat("TimerLimit", 60f);
            Bg[2].DOColor(Color.white, 0.4f);
            Times[2].DOColor(Color.black, 0.4f);
        }
        else
        {
            Bg[2].DOColor(Color.black, 0.4f);
            Times[2].DOColor(Color.grey, 0.4f);
        }
    }
}
