using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public TextMeshProUGUI HSText, HCText, TGText, PCGText, PIGText, PTFGText;

    void Start()
    {  
        HSText.text = PlayerPrefs.GetInt("HighScore").ToString();

        HCText.text = PlayerPrefs.GetInt("HighestCombo").ToString();

        TGText.text = PlayerPrefs.GetInt("totalgames").ToString();

        PCGText.text = PlayerPrefs.GetInt("PerfectGameTxt").ToString();

        PIGText.text = PlayerPrefs.GetInt("PerfectGameImg").ToString();

        PTFGText.text = PlayerPrefs.GetInt("PerfectGameTF").ToString();
    }
}
