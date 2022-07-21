using System;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Audio[] audios;

    //Volume Toggle Dependicies
    public Toggle toggle;
    public RectTransform HandleTransform;
    public Image Bg;


    void Awake()
    {
        foreach (Audio a in audios)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
            a.source.outputAudioMixerGroup = a.group;
        }
    }
    void Start()
    {
        VolCheck();
    }

    public void Play(string name)
    {
        Audio aud = Array.Find(audios, Audio => Audio.Name == name);
        if (aud == null)
        {
            return;
        }
        aud.source.Play();
    }

    public void ToggleVol()
    {

        if (toggle.isOn != true)
        {
            audioMixer.SetFloat("Volume", -80f);
            PlayerPrefs.SetFloat("Vol", -80f);

            HandleTransform.DOAnchorPosX(-60f, 0.4f).SetEase(Ease.InOutBack);
            Bg.DOColor(Color.grey, 0.4f);
        }
        else
        {
            audioMixer.SetFloat("Volume", 0f);
            PlayerPrefs.SetFloat("Vol", 0f);

            HandleTransform.DOAnchorPosX(60f, 0.4f).SetEase(Ease.InOutBack);

            Bg.DOColor(Color.green, 0.4f);
        }
    }
    void VolCheck()
    {
        if (PlayerPrefs.GetFloat("Vol") == -80f)
        {
            audioMixer.SetFloat("Volume", -80f);
            toggle.isOn = false;
            HandleTransform.DOAnchorPosX(-60f, 0.4f).SetEase(Ease.InOutBack);

            Bg.DOColor(Color.grey, 0.4f);
        }
        if (PlayerPrefs.GetFloat("Vol") == 0f)
        {
            audioMixer.SetFloat("Volume", 0f);
            toggle.isOn = true;
            HandleTransform.DOAnchorPosX(60f, 0.4f).SetEase(Ease.InOutBack);

            Bg.DOColor(Color.green, 0.4f);
        }
    }
}
