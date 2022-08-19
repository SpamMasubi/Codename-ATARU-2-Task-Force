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
            GameManager.stage += 1;
        }
    }

    public void loadScene(int index)
    {
        playSound();
        SceneManager.LoadScene(index);
        EnemySpawner.enemiesDefeated = 0;
        TimeCounter.bossAppeared = false;
        BGMMusic.instance.PlaySong(BGMMusic.instance.levelSong);
        if(GameManager.stage > 4)
        {
            ScoreManager.instance.setHighScore();
            Destroy(FindObjectOfType<GameManager>().gameObject);
        }
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }

}
