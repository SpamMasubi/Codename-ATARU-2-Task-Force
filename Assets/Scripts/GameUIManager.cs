using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject gamePlayCanvas, gameOverCanvas;
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

    public void loadScene(int index)
    {
        SceneManager.LoadScene(index);
        EnemySpawner.enemiesDefeated = 0;
    }
}
