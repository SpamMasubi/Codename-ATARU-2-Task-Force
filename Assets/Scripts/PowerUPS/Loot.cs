using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemSpawn
{
    public GameObject item;
    public float spawnRate;
    [HideInInspector]public float minSpawnProb, maxSpawnProb;
}

public class Loot : MonoBehaviour
{
    public itemSpawn[] itemsDrop;
    public GameObject explosionEffect;
    public AudioClip explosionsfx;
    public float timeToDestroy = 1f;//Time after which bullet will be destroy if no collision happens

    private float minHeight = 0f, maxHeight = 0f, minWidth = 0f, maxWidth = 0f;
    private Vector2 minW, maxW;
    private bool isOpen;
    private float timer = 0f;
    private bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        minHeight = Screen.height * 1.05f;
        maxHeight = minHeight;

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

        for (int i = 0; i < itemsDrop.Length; i++)
        {

            if (i == 0)
            {
                itemsDrop[i].minSpawnProb = 0;
                itemsDrop[i].maxSpawnProb = itemsDrop[i].spawnRate - 1;
            }
            else
            {
                itemsDrop[i].minSpawnProb = itemsDrop[i - 1].maxSpawnProb + 1;
                itemsDrop[i].maxSpawnProb = itemsDrop[i].minSpawnProb + itemsDrop[i].spawnRate - 1;
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if ((timer >= timeToDestroy) && !collided)
        {
            timer = 0f;
            Destroy(gameObject);//Destroy Loot Plane;        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerProjectiles"))
        {
            Explosion();
            lootItems();
            Destroy(gameObject);
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
                GameSoundManager.instance.playSFX(explosionsfx, 1f);
            }
        }
    }

    void lootItems()
    {
        float randomNum = Random.Range(0, 100);

        for (int i = 0; i < itemsDrop.Length; i++)
        {
            if(randomNum>=itemsDrop[i].minSpawnProb && randomNum<= itemsDrop[i].maxSpawnProb)
            {
                Instantiate(itemsDrop[i].item, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
