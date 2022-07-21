using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private HighscoreSystem Combo;
    [SerializeField] private LevelLoader levelload;
    [SerializeField] private AudioManager AudioManager;
    [SerializeField] private TextMeshProUGUI questionText, scoreText, timerText, gameEndText;
    [SerializeField] private List<Image> LivesList;
    [SerializeField] private GameObject gameOverPanel, mainMenuPanel, gameMenuPanel, homePanel;
    [SerializeField] public Image questionImage;
    [SerializeField] private List<Button> options, TruFalBtns, UIButtons;
    [SerializeField] private Color wrongCol, correctCol, normalCol, HeartCol;

    public Gradient TimeGrad;
    public Slider TimeSlider;
    public Image fill;
    private Question question;
    private bool answered;
    private Button RightBtn;

    public TextMeshProUGUI ScoreText { get { return scoreText; } }
    public TextMeshProUGUI TimerText { get { return timerText; } }
    public TextMeshProUGUI GameEndText { get { return gameEndText; } }
    public GameObject GameOverPanel { get { return gameOverPanel; } }

    public IEnumerator Pulse()
    {
        for (float i = 1f; i <= 1.15f; i += 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1.15f, 1.15f, 1.15f);

        for (float i = 1.15f; i >= 1f; i -= 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    IEnumerator ClassicLoad()
    {
        AudioManager.Play("Glitch");
        StartCoroutine(levelload.Loadanim());
        yield return new WaitForSeconds(1.5f);
        quizManager.StartGame(0);
        PlayerPrefs.SetInt("classicplay", PlayerPrefs.GetInt("classicplay") + 1);
        PlayerPrefs.SetInt("totalgames", PlayerPrefs.GetInt("totalgames") + 1);
        homePanel.SetActive(false);
        gameMenuPanel.SetActive(true);
    }
    IEnumerator HighScoreLoad()
    {
        AudioManager.Play("Glitch");
        StartCoroutine(levelload.Loadanim());
        yield return new WaitForSeconds(1.5f);
        quizManager.StartGame(0);
        PlayerPrefs.SetInt("totalgames", PlayerPrefs.GetInt("totalgames") + 1);
        quizManager.QuestionLimit = 500;
        Combo.enabled = true;
        homePanel.SetActive(false);
        gameMenuPanel.SetActive(true);
    }
    IEnumerator PictureLoad()
    {
        AudioManager.Play("Glitch");
        StartCoroutine(levelload.Loadanim());
        yield return new WaitForSeconds(1.5f);
        quizManager.StartGame(1);
        PlayerPrefs.SetInt("pictureplay", PlayerPrefs.GetInt("pictureplay") + 1);
        PlayerPrefs.SetInt("totalgames", PlayerPrefs.GetInt("totalgames") + 1);
        homePanel.SetActive(false);
        gameMenuPanel.SetActive(true);
    }
    IEnumerator TFLoad()
    {   
        AudioManager.Play("Glitch");
        StartCoroutine(levelload.Loadanim());
        yield return new WaitForSeconds(1.5f);
        quizManager.StartGame(2);
        PlayerPrefs.SetInt("tfplay", PlayerPrefs.GetInt("tfplay") + 1);
        PlayerPrefs.SetInt("totalgames", PlayerPrefs.GetInt("totalgames") + 1);
        homePanel.SetActive(false);
        gameMenuPanel.SetActive(true);
    }

    void Awake()
    {
        if (Combo.enabled == true)
        {
            Combo.enabled = false;
        }

        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for (int i = 0; i < UIButtons.Count; i++)
        {
            Button localBtn = UIButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for (int i = 0; i < TruFalBtns.Count; i++)
        {
            Button localBtn = TruFalBtns[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;

        switch (question.questionType)
        {
            case QuestionType.TEXT:

                questionImage.transform.parent.gameObject.SetActive(false);

                List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

                for (int i = 0; i < options.Count; i++)
                {
                    options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
                    options[i].name = answerList[i];
                    options[i].image.color = normalCol;
                }

                break;
            case QuestionType.IMAGE:
                ImageHolder();

                questionImage.transform.gameObject.SetActive(true);

                List<string> thatanswerList = ShuffleList.ShuffleListItems<string>(question.options);

                for (int i = 0; i < options.Count; i++)
                {
                    options[i].GetComponentInChildren<TextMeshProUGUI>().text = thatanswerList[i];
                    options[i].name = thatanswerList[i];
                    options[i].image.color = normalCol;
                }

                questionImage.sprite = question.questionImg;
                break;
            case QuestionType.TF:

                questionImage.transform.parent.gameObject.SetActive(false);

                foreach (var answers in options)
                {
                    answers.transform.gameObject.SetActive(false);
                }
                foreach (var opt in TruFalBtns)
                {
                    opt.transform.gameObject.SetActive(true);
                }

                List<string> answer = question.options;

                for (int i = 0; i < TruFalBtns.Count; i++)
                {
                    TruFalBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = answer[i];
                    TruFalBtns[i].name = answer[i];
                    TruFalBtns[i].image.color = normalCol;
                }

                break;
        }
        questionText.text = question.questionInfo;

        


        answered = false;
    }
    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.Playing)
        {
            if (!answered)
            {
                answered = true;
                bool val = quizManager.Answer(btn.name);
                RightBtn = GameObject.Find(question.correctAnswer).GetComponent<Button>();

                if (val)
                {
                    btn.image.color = correctCol;
                }
                else
                {
                    btn.image.color = wrongCol;
                    RightBtn.image.color = correctCol;
                }
            }
        }
        switch (btn.name)
        {
            case "ClassicPlayBtn":
                StartCoroutine(ClassicLoad());
                break;
            case "HighScorePlayBtn":
                StartCoroutine(HighScoreLoad());
                break;
            case "PicturePlayBtn":
                StartCoroutine(PictureLoad());
                break;
            case "True/FalsePlayBtn":
                StartCoroutine(TFLoad());
                break;
        }     
    }
    public void ReduceLife(int index)
    {
        LivesList[index].color = HeartCol;
        LivesList[index].rectTransform.DOSizeDelta(new Vector2(68f, 68f), 0.5f).SetEase(Ease.OutBack);
    }
}
