using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip jumpAudio, hurtAudio, cherryAudio;

    public AudioSource effectAudioSource;
    public AudioSource musicAudioSource;

    private void Awake()
    {
        instance = this;
    }

    public void JumpAudio()
    {
        effectAudioSource.clip = jumpAudio;
        effectAudioSource.Play();
    }

    public void HurtAudio()
    {
        effectAudioSource.clip = hurtAudio;
        effectAudioSource.Play();
    }

    public void CherryAudio()
    {
        effectAudioSource.clip = cherryAudio;
        effectAudioSource.Play();
    }

    public void DisableAudio()
    {
        effectAudioSource.enabled = false;
        musicAudioSource.enabled = false;
    }
}
