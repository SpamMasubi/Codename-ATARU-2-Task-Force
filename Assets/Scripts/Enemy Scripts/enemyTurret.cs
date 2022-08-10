using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTurret : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;

    public float Range;
    public Transform Target;
    bool Detected = false;
    Vector2 Direction;
    public GameObject gun;
    public GameObject bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform shootPoint;
    public GameObject explosionEffect; 
    public GameObject flameEffectR, flameEffectL;
    public AudioClip explosionsfx;

    private int countTilDestroy = 10;

    [Header("Invincibility Flash")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    private bool isInvincible;

    public SpriteRenderer bossSprite;

    private void Start()
    {
        Target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range, whatIsPlayer); //rayInfo will be true only if hit the player.
        if (rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == "Player")
            {
                if (Detected == false)
                {
                    Detected = true;
                }
            }
        }
        else
        {
            if (Detected == true)
            {
                Detected = false;
            }
        }

        if (Detected == true)
        {
            gun.transform.up = -Direction;
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponentInParent<Enemy>())
        {
            if (collision.gameObject.CompareTag("PlayerProjectiles") && GetComponentInParent<Enemy>().isBoss && !isInvincible)
            {
                if (countTilDestroy <= 0)
                {
                    Explosion();
                    if(flameEffectL != null)
                    {
                        flameEffectL.SetActive(true);
                    }
                    else if (flameEffectR != null)
                    {
                        flameEffectR.SetActive(true);
                    }
                    Destroy(gameObject);
                }
                else
                {
                    countTilDestroy -= 1;
                    StartCoroutine(InvincibilityFlash());
                }
            }
        }
    }

    void Explosion()
    {
        //Explosion for epic killing!
        if (explosionEffect == null)
        {
            return;
        }

        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        if (explosionsfx != null)
        {
            if (GameSoundManager.instance != null)
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
            bossSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            bossSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        isInvincible = false;
    }

    void Shoot()
    {
        GameObject bullets = Instantiate(bullet, shootPoint.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
