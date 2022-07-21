using UnityEngine;

public class HighscoreSystem : MonoBehaviour
{
    public int ComboCheck = 0;

    public int ComboScore = 5;

    public int HighScore = 0;

    private void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        if (HighScore >= PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
        if (ComboCheck >= PlayerPrefs.GetInt("HighestCombo"))
        {
            PlayerPrefs.SetInt("HighestCombo", ComboCheck);
        }
    }
}
