using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonBulletProjectiles : MonoBehaviour
{
    public float moveSpeed;

    public bool dontFollow;

    Rigidbody2D rb;

    PlayerController player;

    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerController>();
        if (dontFollow)
        {
            rb.velocity = (-transform.up * moveSpeed) + transform.position;
        }
        else
        {
            moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
        Destroy(gameObject, 3f);
    }
}
