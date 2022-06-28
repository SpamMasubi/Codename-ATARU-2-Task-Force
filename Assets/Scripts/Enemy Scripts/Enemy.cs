using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilesToSpawn; //projectiles to spawn
    public float fireRate = 0.3f;
    public Vector2 projectOffset = new Vector2(0f, -1.3f);
    public Vector2 projectileSpeed = new Vector2(0f, -1f);
    public bool isBoss = false;
    public GameObject healthBar;
    public int scoreToIncrease = 10; //Score increased when enemies killed
    public int ScoreToIncrease { get { return scoreToIncrease; } }
    private bool moveToFro = true;

    private float minHeight = 0f, maxHeight = 0f, minWidth = 0f, maxWidth = 0f;
    private Vector2 vel1, vel2;
    [Range(1f, 20f)]public float smoothTime = 1f;
    private Vector2 minW, maxW;
    private float maxSpeed = 20f;
    private bool closerToMin = false, closerToMax = false, exec = false;
    public enum EnemyType
    {
        Individual, Wave
    }

    public EnemyType enemyType;

    private void Fire()
    {
        Vector2 targetPos = new Vector2(transform.position.x + projectOffset.x, transform.position.y + projectOffset.y);
        GameObject proj = Instantiate(projectilesToSpawn, targetPos, projectilesToSpawn.transform.rotation) as GameObject;
        if (proj.GetComponent<Rigidbody2D>())
        {
            proj.GetComponent<Rigidbody2D>().AddForce(projectileSpeed, ForceMode2D.Impulse);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (projectilesToSpawn != null)
        {
            InvokeRepeating("Fire", 0.0f, fireRate);
        }
        if (healthBar != null)
        {
            healthBar.SetActive(true);
        }
        if (enemyType == EnemyType.Individual)
        {
            minHeight = Screen.height * 0.7f;
            maxHeight = Screen.height * 0.9f;
            Vector2 minH = Camera.main.ScreenToWorldPoint(new Vector2(0f, minHeight));
            Vector2 maxH = Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight));
            float height = Random.Range(minH.y, maxH.y);

            minWidth = Screen.width * 0f;
            maxWidth = Screen.width * 1f;
            minW = Camera.main.ScreenToWorldPoint(new Vector2(minWidth, 0f));
            maxW = Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, 0f));
            minW.y = height;
            maxW.y = height;
            float width = Random.Range(minW.x, maxW.x);

            transform.position = new Vector2(width, height);
        }
    }

    private void Update()
    {
        if(enemyType == EnemyType.Individual && moveToFro)
        {
            if (!exec)
            {
                transform.position = Vector2.SmoothDamp(transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
                if (transform.position.x - 0.1f <= minW.x)
                {
                    closerToMin = true;
                    closerToMax = false;
                    exec = true;
                }
            }
            else
            {
                if (transform.position.x - 0.1f <= minW.x)
                {
                    closerToMin = true;
                    closerToMax = false;
                }
                if (transform.position.x + 0.1f >= maxW.x)
                {
                    closerToMin = false;
                    closerToMax = true;
                }
                if (closerToMax)
                {
                    transform.position = Vector2.SmoothDamp(transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
                }
                else if (closerToMin)
                {
                    transform.position = Vector2.SmoothDamp(transform.position, maxW, ref vel2, smoothTime, maxSpeed, Time.deltaTime);
                }
            }
        }
    }
}
