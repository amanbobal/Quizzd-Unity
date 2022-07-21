using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EasyUI.Toast;

public class ProfileManager : MonoBehaviour
{
    //Username Dependencies
    public TextMeshProUGUI UsernameTextHeader, UsernameTextProfile;
    public TMP_InputField InputField;
    public GameObject UsernamePanel;

    //Avatar Dependencies
    public Image HeaderAvtr, ProfileAvtr;
    public GameObject AvatarPanel;
    public List<Button> Avatars;
    public Transform InstPanel;

    public DialogBox DialogBox;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckUsername());
    }
    private void Awake()
    {
        for (int i = 0; i < Avatars.Count; i++)
        {
            Button Btn = Avatars[i];

            Btn.onClick.AddListener(() => SetAvatar(Btn));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Updating Username
        string str = PlayerPrefs.GetString("Username");
        if (string.IsNullOrEmpty(str) != true)
        {
            UsernameTextHeader.text = PlayerPrefs.GetString("Username");
            UsernameTextProfile.text = PlayerPrefs.GetString("Username");
        }


        //Updating Avatar
        string Avtrstr = PlayerPrefs.GetString("AvatarId");
        if (string.IsNullOrEmpty(Avtrstr) != true)
        {
            Button main = Avatars.Find(obj => obj.name == PlayerPrefs.GetString("AvatarId"));

            Image Avtr = main.GetComponent<Image>();

            HeaderAvtr.sprite = Avtr.sprite;
            ProfileAvtr.sprite = Avtr.sprite;
        }
        
    }

    public IEnumerator CheckUsername()
    {
        string str = PlayerPrefs.GetString("Username", null);
        if (string.IsNullOrEmpty(str) == true)
        {
            yield return new WaitForSeconds(1.5f);

            UsernamePanel.SetActive(true);
        }
        else
        {
            UsernamePanel.SetActive(false);
        }
    }

    public void SetUsername()
    {
        string txt = InputField.text;

        if (string.IsNullOrEmpty(txt) == true)
        {
            Toast.Show("Username Can't be Empty.", 1.5f, ToastColor.Black);
        }
        else
        {
            PlayerPrefs.SetString("Username", InputField.text);

            UsernameTextHeader.text = InputField.text;
            UsernameTextProfile.text = InputField.text;
        }

        StartCoroutine(CheckAvatar());
    }

    public IEnumerator CheckAvatar()
    {
        string str = PlayerPrefs.GetString("AvatarId", null);
        if (string.IsNullOrEmpty(str) == true)
        {
            yield return new WaitForSeconds(0.75f);

            AvatarPanel.SetActive(true);
        }
        else
        {
            AvatarPanel.SetActive(false);
        }
    }
    public void SetAvatar(Button btn)
    {
        PlayerPrefs.SetString("AvatarId", btn.name);

        Image img = btn.GetComponent<Image>();

        HeaderAvtr.sprite = img.sprite;
        ProfileAvtr.sprite = img.sprite;
    }
}