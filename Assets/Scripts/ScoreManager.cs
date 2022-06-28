using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText, highScoreText;
    public static ScoreManager instance;

    private int currentScore = 0;
    private string highScoreKey = "High Score";

    public int Score { get { return currentScore; } }

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
        if(highScoreText != null)
        {
            highScoreText.text = PlayerPrefs.GetInt(highScoreKey).ToString();
        }
    }

    public void setHighScore()
    {
        int HS = PlayerPrefs.GetInt(highScoreKey);
        if(currentScore <= HS)
        {
            return;
        }

        PlayerPrefs.SetInt(highScoreKey, currentScore);
    }

    public void IncreaseScore(int amount)
    {
        //Increase the Score
        if(amount > 0)
        {
            currentScore += amount;
        }
    }
}
