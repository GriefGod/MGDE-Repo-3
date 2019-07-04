using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class Setting_Menu : MonoBehaviour
{//this script is for controlling volumme
    public AudioMixer audioMixer;
    private bool isgyro = true;

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music Volume", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX Volume", volume);
    }

    
}
