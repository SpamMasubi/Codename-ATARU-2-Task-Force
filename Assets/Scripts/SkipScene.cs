using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    public string sceneName;
    private bool isClicked;
    public AudioClip selection;

    public void skipScene()
    {
        if (!isClicked)
        {
            playSound();
            isClicked = true;
            Invoke("LoadNextScene", 1f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        isClicked = false;
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }
}
