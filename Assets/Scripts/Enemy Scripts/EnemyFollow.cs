using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 200f;

    public GameObject bullet;
    public float FireRate;
    public Transform shootPoint;

    float nextTimeToFire = 0;
    float nextTimeToSearch = 0;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }
        else
        {
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = rotateAmount * rotateSpeed;
            rb.velocity = -transform.up * speed;
        }

        if (shootPoint != null)
        {
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
            if (searchPlayer != null)
                target = searchPlayer.transform;
            nextTimeToSearch = Time.time + 0.2f;
        }
    }

    void Shoot()
    {
        GameObject bullets = Instantiate(bullet, shootPoint.position, Quaternion.identity);
    }
}
