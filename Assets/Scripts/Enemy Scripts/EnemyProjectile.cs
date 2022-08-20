using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]protected int damage = 5;
    [SerializeField]protected float timeToDestroy = 1f;//Time after which bullet will be destroy if no collision happens
    [SerializeField]protected bool destroyProjectile = true;
    [SerializeField]protected AudioClip explosionSFX, impact;
    [SerializeField]protected GameObject explosionEffect;
    [SerializeField]protected float explosionEffectLength;

    private bool collided = false;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!destroyProjectile)
        {
            return;
        }
        timer += Time.deltaTime;
        if ((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            Destroy(gameObject);//Destroy bullet;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (explosionEffect != null)
        {
            GameSoundManager.instance.playSFX(explosionSFX);
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject;
            Destroy(explosion, explosionEffectLength);
        }
        collided = true;
        if (collision.gameObject.CompareTag("Player") && !PlayerController.shieldOn)
        {
            GameSoundManager.instance.playSFX(impact);
            collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(damage);
        }
        Destroy(gameObject);
    }
}

