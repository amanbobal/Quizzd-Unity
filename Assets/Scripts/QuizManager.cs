using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;

    [SerializeField] private HighscoreSystem Combo;

    [SerializeField] private AudioManager AudioManager;

    [SerializeField] private List<QuizDataScriptable> QuizData;

    public float TimeLimit;

    [SerializeField] private int ScoreValue = 10;

    private List<Question> questions;

    private Question selectedQuestion;

    private GameStatus gameStatus = GameStatus.Next;

    public GameStatus GameStatus { get { return gameStatus; } }

    private int scoreCount = 0;

    private float currentTime;

    private int QuestionNo = 0;

    public int QuestionLimit = 5;

    private int LivesRemain = 3;

    private int perfectCheckTxt, perfectCheckImg, perfectCheckTF = 0;

    public void StartGame(int index)
    {
        scoreCount = 0;

        if (PlayerPrefs.HasKey("TimerLimit"))
        {
            currentTime = PlayerPrefs.GetFloat("TimerLimit");
        }
        else
        {
            currentTime = TimeLimit;
        }

        LivesRemain = 3;

        questions = new List<Question>();
        for (int i = 0; i < QuizData[index].questions.Count; i++)
        {
            questions.Add(QuizData[index].questions[i]);
        }

        selectQuestion();

        gameStatus = GameStatus.Playing;

        quizUI.TimeSlider.maxValue = currentTime;
        quizUI.TimeSlider.value = currentTime;
        quizUI.fill.color = quizUI.TimeGrad.Evaluate(currentTime);
    }
    void selectQuestion()
    {
        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[val];

        quizUI.SetQuestion(selectedQuestion);

        questions.RemoveAt(val);
    }
    void FixedUpdate()
    {
        if (gameStatus == GameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
            quizUI.TimeSlider.value = currentTime;
            quizUI.fill.color = quizUI.TimeGrad.Evaluate(quizUI.TimeSlider.normalizedValue);
            quizUI.ScoreText.text = "Score:" + scoreCount;
        }

        if (perfectCheckTxt == 5)
        {
            PlayerPrefs.SetInt("PerfectGameTxt", 1);
        }
        if (perfectCheckImg == 5)
        {
            PlayerPrefs.SetInt("PerfectGameImg", 1);
        }
        if (perfectCheckTF == 5)
        {
            PlayerPrefs.SetInt("PerfectGameTF", 1);
        }
    }
    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = time.ToString("ss");

        if (currentTime <= 0)
        {
            gameStatus = GameStatus.Next;
            quizUI.GameOverPanel.SetActive(true);
            quizUI.GameEndText.text = "Game Over!";
            AudioManager.Play("GameOver");
        }
    }
    public bool Answer(string answered)
    {
        bool correctAns = false;

        if (PlayerPrefs.HasKey("TimerLimit"))
        {
            currentTime = PlayerPrefs.GetFloat("TimerLimit");
        }
        else
        {
            currentTime = TimeLimit;
        }

        if (answered == selectedQuestion.correctAnswer)
        {
            //right
            correctAns = true;

            AudioManager.Play("Correct");
            scoreCount += ScoreValue;
            StartCoroutine(quizUI.Pulse());
            quizUI.ScoreText.text = "Score:" + scoreCount;
            
            QuestionNo++;

            if (Combo.enabled == true)
            {
                Combo.ComboCheck++;

                if (Combo.ComboCheck >= 3)
                {
                    scoreCount += Combo.ComboScore;
                }
                if (scoreCount >= Combo.HighScore)
                {
                    Combo.HighScore = scoreCount;
                }
            }

            if (quizUI.TimerText.color != Color.white)
            {
                quizUI.TimerText.color = Color.white;
            }

            if (selectedQuestion.questionType == QuestionType.TEXT && Combo.enabled == false)
            {
                perfectCheckTxt++;
            }
            if (selectedQuestion.questionType == QuestionType.IMAGE)
            {
                perfectCheckImg++;
            }
            if (selectedQuestion.questionType == QuestionType.TF)
            {
                perfectCheckTF++;
            }
        }
        else
        {
            //wrong

            AudioManager.Play("Incorrect");
            QuestionNo++;
            LivesRemain--;
            quizUI.ReduceLife(LivesRemain);

            if (LivesRemain <= 0)
            {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
                quizUI.GameEndText.text = "Game Over!";
                AudioManager.Play("GameOver");
            }

            if (Combo.enabled == true)
            {
                Combo.ComboCheck = 0;
            }

            if (quizUI.TimerText.color != Color.white)
            {
                quizUI.TimerText.color = Color.white;
            }

            if (perfectCheckTxt != 0)
            {
                perfectCheckTxt = 0;
            }
            if (perfectCheckImg != 0)
            {
                perfectCheckImg = 0;
            }
            if (perfectCheckTF != 0)
            {
                perfectCheckTF = 0;
            }
        }

        if (gameStatus == GameStatus.Playing)
        {
            if (questions.Count > 0 && QuestionNo < QuestionLimit)
            {
                Invoke("selectQuestion", 0.5f);
            }
            else
            {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
                quizUI.GameEndText.text = "You Did it!";
                AudioManager.Play("GameCompleted");
            }
        }

        return correctAns;
    }

}
[System.Serializable]
public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImg;
    public List<string> options;
    public string correctAnswer;
}
[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    TF
}
[System.Serializable] 
public enum GameStatus
{
    Next,
    Playing
}