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

    [Header("Invincibility Flash")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    private bool isInvincible;
    public SpriteRenderer playerSprite;

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
            if (GetComponent<Enemy>())
            {
                if (GetComponent<Enemy>().isBoss)
                {
                    if (!isInvincible)
                    {
                        StartCoroutine(InvincibilityFlash());
                        currentHealth -= amount;
                    }
                }
                else
                {
                    currentHealth -= amount;
                }
            }
            else if (GetComponent<PlayerController>())
            {
                if (!isInvincible)
                {
                    StartCoroutine(InvincibilityFlash());
                    currentHealth -= amount;
                }
            }
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
                GameSoundManager.instance.playSFX(explosionsfx);
            }
        }
    }
    private IEnumerator InvincibilityFlash()
    {
        int temp = 0;
        isInvincible = true;
        while (temp < numberOfFlashes)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        isInvincible = false;
    }

    public void Kill()
    {
        Explosion();
        if (GetComponent<Enemy>())
        {
            ScoreManager.instance.IncreaseScore(GetComponent<Enemy>().ScoreToIncrease);
            if (GetComponent<Enemy>().isBoss)
            {
                BGMMusic.instance.StopSong();
                GameObject.FindObjectOfType<GameUIManager>().Win();//Win
                PlayerController.instance.CancelInvoke("Fire");
                GetComponent<Enemy>().CancelInvoke("Fire");
                PlayerController.instance.gameObject.SetActive(false);
            }
            EnemySpawner.enemiesDefeated++; 
            Destroy(gameObject);
        }
        else if (GetComponent<PlayerController>())
        {
            GameStats.instance.loseLives(1);
            PlayerController.instance.CancelInvoke("Fire");
            PlayerController.instance.gameObject.SetActive(false);
            if (FindObjectOfType<GameManager>().numOfLives <= 0)
            {
                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.setHighScore();
                }         
                GameObject.FindObjectOfType<GameUIManager>().GameOver();//Game over
            }
            else
            {
                MonoBehaviour gameMono = GameStats.instance.GetComponent<MonoBehaviour>();
                gameMono.StartCoroutine(PlayerRespawn());
            }
        }
    }
    private IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(5);
        PlayerController.instance.gameObject.SetActive(true);
        currentHealth = maxHealth;
        StartCoroutine(InvincibilityFlash());
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
