using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum PowerUpType
    {
        Ammo, Health
    }

    public PowerUpType powerUpType;
    public int perksToGive = 10;

    private void Start()
    {
        transform.position = new Vector2(Random.Range(0f, 1f), Random.Range(0.9f, 0.95f));
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
