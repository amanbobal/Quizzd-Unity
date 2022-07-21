using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Audio 
{
    public string Name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range (.1f, 5f)]
    public float pitch;

    public bool loop;

    public AudioMixerGroup group;

    [HideInInspector]
    public AudioSource source;
}
