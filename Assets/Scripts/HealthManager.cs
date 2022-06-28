using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int currentHealth;

    public int minHealth = 0, maxHealth = 100;
    public Image healthFill;

    public GameObject explosionEffect;
    public AudioClip explosionsfx;

    public void IncreaseHealth(int amount = 1)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += amount;
        }
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    public void DecreaseHealth(int amount  = 1)
    {
        if(currentHealth <= minHealth)
        {
            Kill();
        }
        if(currentHealth > minHealth)
        {
            currentHealth -= amount;
        }
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }

    void Explosion()
    {
        //Explosion for epic killing!
        if(explosionEffect == null)
        {
            return;
        }

        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        if(explosionsfx != null)
        {
            if(GameSoundManager.instance != null)
            {
                GameSoundManager.instance.playSFX(explosionsfx, 1f);
            }
        }
    }
    private void Kill()
    {
        Explosion();
        if (GetComponent<Enemy>())
        {
            ScoreManager.instance.IncreaseScore(GetComponent<Enemy>().ScoreToIncrease);
            if (GetComponent<Enemy>().isBoss)
            {
                
            }
            EnemySpawner.enemiesDefeated++; 
        }
        else if (GetComponent<PlayerController>())
        {
            if(ScoreManager.instance != null)
            {
                ScoreManager.instance.setHighScore();
            }
            GameObject.FindObjectOfType<GameUIManager>().GameOver();//Game over
        }
        Destroy(gameObject);
    }

    private float FillAmount()
    {
        return (float)currentHealth / (float)maxHealth;
    }
    private void Update()
    {
         if(healthFill != null)
        {
            healthFill.fillAmount = FillAmount();
        }
        if (currentHealth <= minHealth)
        {
            Kill();
        }
       
    }

    private void Awake()
    {
        if (GetComponent<Enemy>())
        {
            if (GetComponent<Enemy>().isBoss)
            {
                Debug.Log("Get Health");
                healthFill = FindObjectOfType<Canvas>().transform.Find("BossHealthBar/HealthFill").GetComponent<Image>();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
}
