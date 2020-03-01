using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void Start()
    {
        audioMixer.SetFloat("volume", (float)-40);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("volume", value);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isToggled)
    {
        Screen.fullScreen = isToggled;
    }
}
