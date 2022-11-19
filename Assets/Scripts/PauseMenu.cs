using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject resumeButton, quitButton;
    public AudioClip selection;

    public static bool isPause;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Enable Debug Button 1") || Input.GetKeyDown(KeyCode.Escape))
        {
            playSound();
            PauseUnPause();
        }
    }

    public void PauseUnPause()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            isPause = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
#if UNITY_STANDALONE || UNITY_WEBGL
            //Clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(resumeButton);
#endif
        }
        else
        {
            isPause = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        playSound();
        isPause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMainMenu(int index)
    {
        playSound();
        isPause = false;
        if (selectStage.replay)
        {
            SceneManager.LoadScene(10);
        }
        else
        {
            SceneManager.LoadScene(index);
        }
        Time.timeScale = 1f;
        EnemySpawner.enemiesDefeated = 0;
        TimeCounter.bossAppeared = false;
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }
}
