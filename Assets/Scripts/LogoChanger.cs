using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;

public class LogoChanger : MonoBehaviour
{
    public AudioMixer audioMixer;

    public VideoPlayer player;

    public VideoClip[] clips;

    void Awake()
    {
        int random = Random.Range(0, clips.Length);

        player.clip = clips[random];
    }
    private void Start()
    {
        if (PlayerPrefs.GetFloat("Vol") == -80f)
        {
            audioMixer.SetFloat("Volume", -80f);
        }
        if (PlayerPrefs.GetFloat("Vol") == 0f)
        {
            audioMixer.SetFloat("Volume", 0f);
        }
    }
}
