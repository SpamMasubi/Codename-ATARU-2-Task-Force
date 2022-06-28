using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]protected int damage = 5;
    [SerializeField] protected float timeToDestroy = 1f;//Time after which bullet will be destroy if no collision happens
    [SerializeField] protected bool destroyProjectile = true;
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
        collided = true;
        if (collision.gameObject.GetComponent<HealthManager>())
        {
            collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(damage);
        }
        Destroy(gameObject);
    }
}

