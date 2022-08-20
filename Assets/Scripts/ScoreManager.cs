using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText, highScoreText, yourScoreText;
    public static ScoreManager instance;

    private string highScoreKey = "High Score";

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(scoreText != null)
        {
            scoreText.text = FindObjectOfType<GameManager>().scores.ToString();
        }
        if(highScoreText != null)
        {
            highScoreText.text = PlayerPrefs.GetInt(highScoreKey).ToString();
        }
        if(yourScoreText != null)
        {
            yourScoreText.text = yourScore().ToString();
        }
    }

    public void setHighScore()
    {
        int HS = PlayerPrefs.GetInt(highScoreKey);
        if(FindObjectOfType<GameManager>().scores <= HS)
        {
            return;
        }
        PlayerPrefs.SetInt(highScoreKey, FindObjectOfType<GameManager>().scores);
    }

    public int yourScore()
    {
        int yourScores = 0;
        if (FindObjectOfType<GameManager>())
        {
            yourScores = FindObjectOfType<GameManager>().scores;
        }
        return yourScores;
    }

    public void IncreaseScore(int amount)
    {
        //Increase the Score
        if(amount > 0)
        {
            FindObjectOfType<GameManager>().scores += amount;
        }
    }
}
