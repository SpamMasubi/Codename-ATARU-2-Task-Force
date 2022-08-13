using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int damage = 5;
    public GameObject projectilesToSpawn; //projectiles to spawn
    public float fireRate = 0.3f;
    public Vector2 projectOffset = new Vector2(0f, -1.3f);
    public Vector2 projectileSpeed = new Vector2(0f, -1f);
    public float offset;
    public bool isBoss = false;
    public bool fireOnce = false;
    public GameObject healthBar;
    public int scoreToIncrease = 10; //Score increased when enemies killed
    public int ScoreToIncrease { get { return scoreToIncrease; } }
    private bool moveToFro = true;

    [Header("Boss Attributes")]
    public GameObject specialProjectiles;
    public float specialAttackTime = 0f;
    float specialAttackTimer = 0f;
    public Transform cannon;
    public float cannonReadyTime = 0f;
    float cannonReadyTimer = 0f;
    //missiles projectiles (if uses missiles for double)
    public bool doubleMissiles;
    public GameObject missiles;
    public Vector2 missileProjectOffsetR = new Vector2(0f, -1.3f);
    public Vector2 missileProjectOffsetL = new Vector2(0f, -1.3f);
    public Vector2 missileSpeed = new Vector2(0f, 1f);

    private float minHeight = 0f, maxHeight = 0f, minWidth = 0f, maxWidth = 0f;
    private Vector2 vel1, vel2;
    [Range(1f, 20f)]public float smoothTime = 1f;
    private Vector2 minW, maxW;
    private float maxSpeed = 20f;
    private bool closerToMin = false, closerToMax = false, exec = false;
    public enum EnemyType
    {
        Individual, Wave, Ground, StraightFlight, None
    }

    public EnemyType enemyType;

    private void Fire()
    {
        if (doubleMissiles)
        {
            Vector2 targetPosR = new Vector2(transform.position.x + missileProjectOffsetR.x, transform.position.y + missileProjectOffsetR.y);
            Vector2 targetPosL = new Vector2(transform.position.x + missileProjectOffsetL.x, transform.position.y + missileProjectOffsetL.y);
            GameObject projR = Instantiate(missiles, targetPosR, missiles.transform.rotation) as GameObject;
            GameObject projL = Instantiate(missiles, targetPosL, missiles.transform.rotation) as GameObject;
            if (projR.GetComponent<Rigidbody2D>() && projL.GetComponent<Rigidbody2D>())
            {
                projR.GetComponent<Rigidbody2D>().AddForce(missileSpeed, ForceMode2D.Impulse);
                projL.GetComponent<Rigidbody2D>().AddForce(missileSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            Vector2 targetPos = new Vector2(transform.position.x + projectOffset.x, transform.position.y + projectOffset.y);
            GameObject proj = Instantiate(projectilesToSpawn, targetPos, projectilesToSpawn.transform.rotation) as GameObject;
            if (proj.GetComponent<Rigidbody2D>())
            {
                proj.GetComponent<Rigidbody2D>().AddForce(projectileSpeed, ForceMode2D.Impulse);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(cannon != null)
        {
            cannon.GetComponent<Animator>().Rebind();
        }
        if (projectilesToSpawn != null && !fireOnce)
        {
            InvokeRepeating("Fire", 0.0f, fireRate);
        }
        else if(projectilesToSpawn != null && fireOnce)
        {
            Invoke("Fire", 1.5f);
        }
        if (enemyType == EnemyType.Individual || enemyType == EnemyType.StraightFlight || enemyType == EnemyType.Ground)
        {
            if (isBoss)
            {
                minHeight = Screen.height * 0.85f;
                maxHeight = minHeight;
            }
            else if (enemyType == EnemyType.StraightFlight || enemyType == EnemyType.Ground)
            {
                minHeight = Screen.height * 1.02f;
                maxHeight = minHeight;
            }
            else
            {
                minHeight = Screen.height * 0.8f;
                maxHeight = Screen.height * 0.9f;
            }
            Vector2 minH = Camera.main.ScreenToWorldPoint(new Vector2(0f, minHeight));
            Vector2 maxH = Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight));
            float height = Random.Range(minH.y, maxH.y);
            if (enemyType == EnemyType.Ground && GameManager.stage == 4)
            {
                minWidth = Screen.width * 0.2f;
                maxWidth = Screen.width * 0.7f;
            }
            else
            {
                minWidth = Screen.width * 0f;
                maxWidth = Screen.width * 1f;
            }
            minW = Camera.main.ScreenToWorldPoint(new Vector2(minWidth, 0f));
            maxW = Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, 0f));
            minW.y = height;
            maxW.y = height;
            float width = Random.Range(minW.x, maxW.x);

            transform.position = new Vector2(width, height);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(damage);
        }
    }

    public void enableHealth()
    {
        GetComponent<HealthManager>().enabled = true;
    }

    public void StartBoss()
    {
        GetComponent<Enemy>().enabled = true;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    private void Update()
    {
        if (enemyType == EnemyType.Individual && moveToFro)
        {
            if (!exec)
            {
                transform.position = Vector2.SmoothDamp(transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
                if (transform.position.x - offset <= minW.x)
                {
                    closerToMin = true;
                    closerToMax = false;
                    exec = true;
                }
            }
            else
            {
                if (transform.position.x - offset <= minW.x)
                {
                    closerToMin = true;
                    closerToMax = false;
                }
                if (transform.position.x + offset >= maxW.x)
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

        if (isBoss)
        {
            //Special attack for boss
            if (cannon != null)
            {
                if (specialAttackTimer <= 0f && cannonReadyTimer <= 0f)
                {
                    specialAttackTimer = specialAttackTime;
                    cannonReadyTimer = cannonReadyTime;
                }
                if (cannonReadyTimer > 0f)
                {
                    cannonReadyTimer -= Time.deltaTime;
                }
                else
                {
                    if (specialAttackTimer > 0f && cannonReadyTimer <= 0f)
                    {
                        cannon.GetComponent<Animator>().SetBool("OpenClose", true);
                        specialAttackTimer -= Time.deltaTime;
                        if (specialAttackTimer <= 0f)
                        {
                            Vector2 targetPos = new Vector2(transform.position.x + projectOffset.x, transform.position.y + projectOffset.y);
                            GameObject specialProj = Instantiate(specialProjectiles, targetPos, specialProjectiles.transform.rotation) as GameObject;
                            if (specialProj.GetComponent<Rigidbody2D>())
                            {
                                specialProj.GetComponent<Rigidbody2D>().AddForce(projectileSpeed, ForceMode2D.Impulse);
                            }
                            cannon.GetComponent<Animator>().SetBool("OpenClose", false);
                        }
                    }
                }
            }
            else {
                if (specialAttackTimer <= 0f)
                {
                    specialAttackTimer = specialAttackTime;
                }
                if (specialAttackTimer > 0f)
                {
                    specialAttackTimer -= Time.deltaTime;
                    if (specialAttackTimer <= 0f)
                    {
                        Vector2 targetPos = new Vector2(transform.position.x + projectOffset.x, transform.position.y + projectOffset.y);
                        GameObject specialProj = Instantiate(specialProjectiles, targetPos, specialProjectiles.transform.rotation) as GameObject;
                        if (specialProj.GetComponent<Rigidbody2D>())
                        {
                            specialProj.GetComponent<Rigidbody2D>().AddForce(projectileSpeed, ForceMode2D.Impulse);
                        }
                    }
                }
            }
        }
    }
}
