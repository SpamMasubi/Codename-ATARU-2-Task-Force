using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public Text timeUI;//refernce to the time counter UI Text
    public static bool bossAppeared = false;

    public float startTime = 5;//the time when the user clicks on play
    float currentTime; //the ellapsed time after the user clicks on play
    private bool exec = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }


    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timeUI.text = currentTime.ToString("00");

        if (currentTime <= 0)
        {
            currentTime = 0;
            if (!exec)
            {
                bossAppeared = true;
                //Spawn Boss
                FindObjectOfType<EnemySpawner>().spawnEnemyBoss();
                exec = true;
                gameObject.SetActive(false);
            }
        }
    }
}
