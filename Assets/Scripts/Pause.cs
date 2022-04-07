using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public AudioMixer audioMixer;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
}