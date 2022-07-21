using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementItemController : MonoBehaviour
{
    [SerializeField] Image TrophyImg;

    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI DescriptionText;
    [SerializeField] Color LockedCol;


    public bool unlocked;
    public Achievement achievement;

    public void RefreshView()
    {
        TitleText.text = achievement.title;
        DescriptionText.text = achievement.description;

        if (unlocked)
        {
            TrophyImg.color = Color.white;
        }
        else
        {
            TrophyImg.color = LockedCol;
        }
    }

    private void OnValidate()
    {
        RefreshView();
    }

}
