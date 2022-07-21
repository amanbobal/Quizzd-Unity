using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementDatabase database;

    public GameObject AchievementPrefab;

    public Transform Panel;

    public HighscoreSystem combo;

    [SerializeField][HideInInspector]
    private List<AchievementItemController> AchievementItems;

    void Start()
    {
        LoadAchievements();
    }

    [ContextMenu("LoadAchievements")]
    private void LoadAchievements()
    {
        foreach (AchievementItemController item in AchievementItems)
        {
            DestroyImmediate(item.gameObject);
        }
        AchievementItems.Clear();
        foreach (Achievement achievement in database.Achievements)
        {
            GameObject obj = Instantiate(AchievementPrefab, Panel);
            AchievementItemController controller = obj.GetComponent<AchievementItemController>();
            bool unlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1;
            controller.unlocked = unlocked;
            controller.achievement = achievement;
            controller.RefreshView();
            AchievementItems.Add(controller);
        }
    }

    public void UnlockAchievement(Achievements achievement)
    {
        AchievementItemController item = AchievementItems[(int)achievement];
        if (item.unlocked)
        {
            return;
        }
        PlayerPrefs.SetInt(item.achievement.id, 1);
        PlayerPrefs.SetInt("unlockedachievements", PlayerPrefs.GetInt("unlockedachievements") + 1);
        item.unlocked = true;
        item.RefreshView();
    }

    private void FixedUpdate()
    {
        CheckAchievements();
    }

    private void CheckAchievements()
    {
        if (PlayerPrefs.GetInt("classicplay") == 1)
        {
            UnlockAchievement(Achievements.classic1);
        }

        if (PlayerPrefs.GetInt("pictureplay") == 1)
        {
            UnlockAchievement(Achievements.picture1);
        }

        if (PlayerPrefs.GetInt("tfplay") == 1)
        {
            UnlockAchievement(Achievements.truefalse1);
        }

        if (PlayerPrefs.GetInt("classicplay") == 10)
        {
            UnlockAchievement(Achievements.classic10);
        }

        if (PlayerPrefs.GetInt("pictureplay") == 10)
        {
            UnlockAchievement(Achievements.picture10);
        }

        if (PlayerPrefs.GetInt("tfplay") == 10)
        {
            UnlockAchievement(Achievements.truefalse10);
        }

        if (PlayerPrefs.GetInt("HighScore") >= 100)
        {
            UnlockAchievement(Achievements.score100);
        }

        if (PlayerPrefs.GetInt("HighScore") >= 500)
        {
            UnlockAchievement(Achievements.score500);
        }

        if (PlayerPrefs.GetInt("HighScore") >= 1000)
        {
            UnlockAchievement(Achievements.score1000);
        }

        if (combo.ComboCheck == 5)
        {
            UnlockAchievement(Achievements.combo5);
        }

        if (combo.ComboCheck == 10)
        {
            UnlockAchievement(Achievements.combo10);
        }

        if (combo.ComboCheck == 25)
        {
            UnlockAchievement(Achievements.combo25);
        }

        if (PlayerPrefs.GetInt("classicplay") == 69 && PlayerPrefs.GetInt("pictureplay") == 69 && PlayerPrefs.GetInt("tfplay") == 69)
        {
            UnlockAchievement(Achievements.sus);
        }

        if (PlayerPrefs.GetInt("PerfectGameTxt") == 1 && PlayerPrefs.GetInt("PerfectGameImg") == 1 && PlayerPrefs.GetInt("PerfectGameTF") == 1)
        {
            UnlockAchievement(Achievements.scoreperfect);
        }

        if (PlayerPrefs.GetInt("unlockedachievements") == 14)
        {
            UnlockAchievement(Achievements.allcomplete);
        }
    }
}
