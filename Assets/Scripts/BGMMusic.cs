using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMusic : MonoBehaviour
{
    public static BGMMusic instance;  
    private AudioSource audioS;
    public AudioClip levelSong, bossSong;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        PlaySong(levelSong);
    }

    private void Update()
    {
        
        if (PauseMenu.isPause)
        {
            audioS.Pause();
        }
        else
        {
            audioS.UnPause();
        }
    }

    public void StopSong()
    {
        audioS.Stop();
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.volume = GameMenuManager.musicVolumeSet;
        audioS.clip = clip;
        audioS.Play();
    }
}
