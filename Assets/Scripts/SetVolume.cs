using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    private AudioSource audioVolume;
    public bool isMusic;

    private void Start()
    {
        audioVolume = GetComponent<AudioSource>();
        if (isMusic)
        {
            audioVolume.volume = GameMenuManager.musicVolumeSet;
        }
        else
        {
            audioVolume.volume = GameMenuManager.sfxVolumeSet;
        }
    }
}
