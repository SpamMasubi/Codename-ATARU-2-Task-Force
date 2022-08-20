using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager GM;
    public static GameManager instance;
    public static int stage = 1;
    //[HideInInspector]
    public int numOfMissiles = 4, numOfLives = 3, scores = 0;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void resetGame()
    {
        numOfMissiles = 4;
        numOfLives = 3;
        scores = 0;
    }

}
