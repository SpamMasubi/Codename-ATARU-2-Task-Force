using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxEnemies = 3;//Max number of enemies at a time, We are going to count enemy wave as a single enemy
    public GameObject[] enemiesToSpawn = new GameObject[2];
    public GameObject enemyBoss;
    public GameObject bossHealthBar;
    public GameObject dialogue; 
    public static int enemiesDefeated = 0;

    float secondsBetweenSpawn = 1.2f;
    float elapsedTime = 0.0f;
    private int getNumberOfEnemies()
    {
        int number = 0;
        Enemy[] temp = GameObject.FindObjectsOfType<Enemy>();
        for(int i = 0; i < temp.Length; i++)
        {
            if (temp[i].enemyType == Enemy.EnemyType.Individual || temp[i].enemyType == Enemy.EnemyType.StraightFlight || temp[i].enemyType == Enemy.EnemyType.Ground)
            {
                number++;
            }
            else if(temp[i].enemyType == Enemy.EnemyType.Wave)
            {
                number += GameObject.FindObjectsOfType<EnemyWave>().Length;
            }
        }
        return number;
    }

    private void spawnEnemies()
    {
        if (getNumberOfEnemies() >= maxEnemies)
        {
            return;
        }

        int n = Random.Range(0, enemiesToSpawn.Length);
        for (int i = getNumberOfEnemies(); i < maxEnemies; ++i)
        {
            if (enemiesToSpawn[n] != null)
            {
                if (!TimeCounter.bossAppeared)
                {
                    elapsedTime += Time.deltaTime;

                    if (elapsedTime > secondsBetweenSpawn)
                    {
                        elapsedTime = 0;
                        Instantiate(enemiesToSpawn[n]);
                    }
                }
            }
            n = Random.Range(0, enemiesToSpawn.Length);
        }
    }

    public void spawnEnemyBoss()
    {
        if(enemyBoss != null)
        {
            BGMMusic.instance.PlaySong(BGMMusic.instance.bossSong);
            Instantiate(enemyBoss);
            bossHealthBar.SetActive(true);
            dialogue.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(bossHealthBar != null)
        {
            bossHealthBar.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!TimeCounter.bossAppeared)
        {
            Invoke("spawnEnemies", 1f);
        }
    }
}
