using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionEffectLength;
    public int damage = 10;
    public float timeToDestroy = 1f;//Time after which bullet will be destroy if no collision happens
    public AudioClip explosionSFX;
    private bool collided = false;

    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if ((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            Destroy(gameObject);//Destroy bullet;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        GameSoundManager.instance.playSFX(explosionSFX);
        if(explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject;
            Destroy(explosion, explosionEffectLength);
        }
        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<PolygonCollider2D>().enabled)
            {
                collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(damage);
            }
        }
        Destroy(gameObject);//Destroy bullet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Enemy"))
        {
            GameSoundManager.instance.playSFX(explosionSFX);
            if (explosionEffect != null)
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject;
                Destroy(explosion, explosionEffectLength);

            }
            Destroy(gameObject);//Destroy bullet;
        }
    }
}
