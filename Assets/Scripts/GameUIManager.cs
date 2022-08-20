using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameUIManager : MonoBehaviour
{
    public GameObject gamePlayCanvas, gameOverCanvas, winCanvas;
    public GameObject nextStageButton, replayButton;
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
        GameManager.instance.resetGame();
        BGMMusic.instance.StopSong();
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(replayButton);
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
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(nextStageButton);
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
            ScoreManager.instance.yourScore();
            Destroy(FindObjectOfType<GameManager>().gameObject);
        }
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }

}
