using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float timeToDestroy = 1f;//Time after which bullet will be destroy if no collision happens
    public int damage = 1;

    private bool collided = false;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            PlayerController.instance.ReleaseBullet(gameObject);//Destroy bullet;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if (collision.gameObject.GetComponent<HealthManager>())
        {
            collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(damage);
        }
        PlayerController.instance.ReleaseBullet(gameObject);//Destroy bullet;
    }
}
