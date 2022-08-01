using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public static GameStats instance;
    public Text missileText, livesText;

    private void Start()
    {
        missileText = GameObject.Find("MAText").GetComponent<Text>();
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
    }
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(missileText != null)
        {
            missileText.text = FindObjectOfType<GameManager>().numOfMissiles.ToString();
        }
        if(livesText != null)
        {
            livesText.text = FindObjectOfType<GameManager>().numOfLives.ToString();
        }
    }
    public bool checkCanShootMissile(int amount)
    {
        bool p = false;
        if(FindObjectOfType<GameManager>().numOfMissiles - amount >= 0)
        {
            p = true;
        }

        return p;
    }
    public void shootMissileByAmount(int amount)
    {
        if(FindObjectOfType<GameManager>().numOfMissiles - amount >= 0)
        {
            FindObjectOfType<GameManager>().numOfMissiles -= amount;
        }
    }

    public void addMissileByAmount(int amount)
    {
        if (amount >= 0)
        {
            FindObjectOfType<GameManager>().numOfMissiles += amount;
        }
    }    
    public void loseLives(int amount)
    {
        if(FindObjectOfType<GameManager>().numOfLives - amount >= 0)
        {
            FindObjectOfType<GameManager>().numOfLives -= amount;
        }
    }

    public void addLivesByAmount(int amount)
    {
        if (amount >= 0)
        {
            FindObjectOfType<GameManager>().numOfLives += amount;
        }
    }
}
