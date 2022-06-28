using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public static GameSoundManager instance;

    private AudioSource AS;
    private void Awake()
    {
        instance = this;
        if (!GetComponent<AudioSource>())
        {
            gameObject.AddComponent<AudioSource>();
        }
        AS = GetComponent<AudioSource>();
        AS.volume = 1f;
        AS.loop = false;
        AS.playOnAwake = false;
        AS.clip = null;
    }

    public void playSFX(AudioClip sounds, float volumeScale)
    {
        if(sounds != null)
        {
            AS.PlayOneShot(sounds, volumeScale);
        }
    }

}
