using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxEnemies = 3;//Max number of enemies at a time, We are going to count enemy wave as a single enemy
    public GameObject[] enemiesToSpawn = new GameObject[2];
    public int enemiesToSpawnBoss = 5; //The number of enemies we ahve to defeat to get to the enemy boss
    public GameObject enemyBoss;
    public GameObject bossHealthBar;
    public static int enemiesDefeated = 0;
    
    private int interval = 20;
    private int getNumberOfEnemies()
    {
        int number = 0;
        Enemy[] temp = GameObject.FindObjectsOfType<Enemy>();
        for(int i = 0; i < temp.Length; i++)
        {
            if(temp[i].enemyType == Enemy.EnemyType.Individual)
            {
                number++;
            }
        }
        number += GameObject.FindObjectsOfType<EnemyWave>().Length;
        return number;
    }

    private void spawnEnemies()
    {
        if (getNumberOfEnemies() >= maxEnemies)
        {
            return;
        }

        int n = Random.Range(0, enemiesToSpawn.Length);
        int i = getNumberOfEnemies();
        for (; i <= maxEnemies; ++i)
        {
            n = Random.Range(0, enemiesToSpawn.Length);
            if (enemiesToSpawn[n] != null)
            {
                Instantiate(enemiesToSpawn[n]);
            }
        }
    }

    private void spawnEnemyBoss()
    {
        if(enemyBoss != null)
        {
            BGMMusic.instance.PlaySong(BGMMusic.instance.bossSong);
            Instantiate(enemyBoss);
            bossHealthBar.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawnEnemies", 1f);
        if(bossHealthBar != null)
        {
            bossHealthBar.SetActive(false);
        }
    }

    private bool exec = false;
    // Update is called once per frame
    void Update()
    {
        if (enemiesDefeated >= enemiesToSpawnBoss)
        {
            if (!exec)
            {
                //Spawn Boss
                spawnEnemyBoss();
                exec = true;
            }
           
        }
        else
        {
            if (Time.frameCount % interval == 0)
            {
                Invoke("spawnEnemies", 1f);
            }
        }
    }
}
