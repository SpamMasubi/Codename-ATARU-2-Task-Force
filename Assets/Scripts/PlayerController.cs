using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;
	public float speed = 10f, speedAccX = 20f, speedAccY = 2.5f;
	public vButtons bulletButton, missileButton;
	public bool shootBulletAutomatically = true;//for Tilt control, jet will shoot bullet autmatically

	private bool bulletPressed, missilePressed;
	private float bulletInterval = 0.3f;
	private float counter = 0f;

	//bullets
	public GameObject bullet;
	public Vector2 bulletSpeed = new Vector2(0f, 1f);
	public Vector3 spawnOffset;
	public float fireBulletRate = 0.2f;
	public int bulletPoolSize = 30;

	//missiles
	public GameObject missiles;
	public GameObject launcherR, launcherL;
	public Vector2 missileSpeed = new Vector2(0f, 1f);
	public float fireMissileRate = 0.2f;
	public int missilePoolSize = 30;

    public GameMenuManager.PlayerMovementInputType movementInputType;

	private ObjectPool bulletPool;
	private ObjectPool missilePool;

    public void Awake()
    {
		instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
		bulletPool = new ObjectPool(bullet, bulletPoolSize, "PlayerBulletPool");
		missilePool = new ObjectPool(missiles, missilePoolSize, "PlayerMissilePool");
		movementInputType = GameMenuManager.instance.currentPMIT;
	}

	// Update is called once per frame
	void Update()
	{
		GetInput();
		if (!PauseMenu.isPause)
		{
			if (movementInputType == GameMenuManager.PlayerMovementInputType.ButtonControl)
			{
#if UNITY_STANDALONE || UNITY_WEBGL

				float x = Input.GetAxisRaw("Horizontal");//the value will be -1, 0 or 1 (for left, no input, and right)
				float y = Input.GetAxisRaw("Vertical");//the value will be -1, 0 or 1 (for down, no input, and up)

				//now based on the input we compute a direction vector, and we normalize it to get a unit vector
				Vector2 direction = new Vector2(x, y).normalized;

				//now we call the function that computes and sets the player's position
				Move(direction);

				if (Input.GetButtonDown("Fire1"))
				{
					InvokeRepeating("Fire", 0.0f, fireBulletRate);
				}
				else if (Input.GetButtonUp("Fire1"))
				{
					CancelInvoke("Fire");
				}

				if (Input.GetButtonDown("Fire2"))
				{
					MissileFire();
				}
#endif


#if UNITY_ANDROID || UNITY_IOS
			if(bulletPressed && bulletButton.value1)
            {
				InvokeRepeating("Fire", 0.0f, fireBulletRate);
				bulletButton.value1 = false;
			}
			else if (!bulletPressed && !bulletButton.value1)
            {
				CancelInvoke("Fire");
            }

			if(missilePressed && missileButton.value1)
            {
				MissileFire();
				missileButton.value1 = false;
            }
#endif
			}
			else if (movementInputType == GameMenuManager.PlayerMovementInputType.MouseControl)
			{
				Vector2 rawPos = Input.mousePosition;
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(rawPos);

				//find the screen limits to the player's movement (left, right, top and bottom edges of the screen)
				Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //this is the bottom-left point (corner) of the screen
				Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //this is the top-right point (corner) of the screen

				max.x = max.x - 2.5f; //subtract the player sprite half width
				min.x = min.x + 2.5f; //add the player sprite half width

				max.y = max.y - 2.5f; //subtract the player sprite half height
				min.y = min.y + 2.5f; //add the player sprite half height

				//Get the player's current position
				Vector2 pos = Vector2.Lerp(transform.position, worldPos, speed * Time.deltaTime); ;

				//Make sure the new position is outside the screen
				pos.x = Mathf.Clamp(pos.x, min.x, max.x);
				pos.y = Mathf.Clamp(pos.y, min.y, max.y);

				//Update the player's position
				transform.position = pos;
				if (shootBulletAutomatically)
				{
					counter += Time.deltaTime;
					if (counter >= bulletInterval)
					{
						Fire();
						counter = 0;
					}
				}

				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					InvokeRepeating("Fire", 0.0f, fireBulletRate);
				}
				else if (Input.GetKeyUp(KeyCode.Mouse0))
				{
					CancelInvoke("Fire");
				}

				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					MissileFire();
				}
			}
			else
			{
				//Tilt Control
				transform.Translate(speedAccX * Time.deltaTime * Input.acceleration.x, speedAccY * Time.deltaTime * Input.acceleration.y, 0f);
			}
		}

	}

	void GetInput()
    {
		if(bulletButton != null)
        {
			bulletPressed = bulletButton.value;
        }
		if(missileButton != null)
        {
			missilePressed = missileButton.value;
        }
    }

	void Fire()
	{
		GameObject bulletInstantiate = bulletPool.GetInstance();
		bulletInstantiate.transform.position = transform.position + spawnOffset;
		bulletInstantiate.GetComponent<Rigidbody2D>().AddForce(bulletSpeed, ForceMode2D.Impulse);
	}
	
	void MissileFire()
    {
		if (GameStats.instance.checkCanShootMissile(1))
		{
			GameObject missileInstantiateR = missilePool.GetInstance();
			GameObject missileInstantiateL = missilePool.GetInstance();
			missileInstantiateR.transform.position = launcherR.transform.position;
			missileInstantiateR.GetComponent<Rigidbody2D>().AddForce(missileSpeed, ForceMode2D.Impulse);
			missileInstantiateL.transform.position = launcherL.transform.position;
			missileInstantiateL.GetComponent<Rigidbody2D>().AddForce(missileSpeed, ForceMode2D.Impulse);
			GameStats.instance.shootMissileByAmount(1);
		}
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (FindObjectOfType<Enemy>()) {
			if (collision.gameObject.CompareTag("Enemy") && !FindObjectOfType<Enemy>().isBoss)
			{
				collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(10000);
			}
		}
	}

	public void ReleaseBullet(GameObject bullet)
    {
		bulletPool.ReturnInstance(bullet);
    }
	
	public void ReleaseMissile(GameObject missile)
    {
		missilePool.ReturnInstance(missile);
    }

	void Move(Vector2 direction)
	{
		//find the screen limits to the player's movement (left, right, top and bottom edges of the screen)
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //this is the bottom-left point (corner) of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //this is the top-right point (corner) of the screen

		max.x = max.x - 2.5f; //subtract the player sprite half width
		min.x = min.x + 2.5f; //add the player sprite half width

		max.y = max.y - 2.5f; //subtract the player sprite half height
		min.y = min.y + 2.5f; //add the player sprite half height
		
		//Get the player's current position
		Vector2 pos = transform.position;

		//Calculate the new position
		pos += direction * speed * Time.deltaTime;

		//Make sure the new position is outside the screen
		pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		pos.y = Mathf.Clamp(pos.y, min.y, max.y);

		//Update the player's position
		transform.position = pos;
	}
}
