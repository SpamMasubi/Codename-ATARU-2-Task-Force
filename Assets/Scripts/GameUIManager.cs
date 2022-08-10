using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject gamePlayCanvas, gameOverCanvas, winCanvas;
    public AudioClip selection;
    // Start is called before the first frame update
    void Start()
    {
        if(gamePlayCanvas != null)
        {
            gamePlayCanvas.SetActive(true);
        }
        if(gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        if(winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }

    public void GameOver()
    {
        if (gamePlayCanvas != null)
        {
            gamePlayCanvas.SetActive(false);
        }
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }
    public void Win()
    {
        if (gamePlayCanvas != null)
        {
            gamePlayCanvas.SetActive(false);
        }
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }
    }

    public void loadScene(int index)
    {
        playSound();
        SceneManager.LoadScene(index);
        EnemySpawner.enemiesDefeated = 0;
        TimeCounter.bossAppeared = false;
        GameManager.stage += 1;
        BGMMusic.instance.PlaySong(BGMMusic.instance.levelSong);
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }

}
