using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum PowerUpType
    {
        Ammo, Health, OneUP, Shield
    }

    public PowerUpType powerUpType;
    public int perksToGive = 10;
    public float timeToDestroy = 1f;//Time after which powerups will be destroy if no collision happens

    private float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            timer = 0f;
            Destroy(gameObject);//Destroy Loot Plane;        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(powerUpType == PowerUpType.Ammo)
            {
                //increase ammo
                if(GameStats.instance != null)
                {
                    GameStats.instance.addMissileByAmount(perksToGive);
                }
            }
            else if(powerUpType == PowerUpType.OneUP)
            {
                //Gain 1-Up
                if(GameStats.instance != null)
                {
                    GameStats.instance.addLivesByAmount(perksToGive);
                }
            }
            else if (powerUpType == PowerUpType.Shield)
            {
                //activate shield
                if (PlayerController.instance != null)
                {
                    PlayerController.instance.activateShield(perksToGive);
                }
            }
            else
            {
                //increase Health
                if (collision.gameObject.GetComponent<HealthManager>())
                {
                    collision.gameObject.GetComponent<HealthManager>().IncreaseHealth(perksToGive);
                }
            }
            Destroy(gameObject);
        }
    }
}
