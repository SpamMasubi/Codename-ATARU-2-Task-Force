using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPSpawners : MonoBehaviour
{
    public GameObject[] powerUps = new GameObject[1];
    [Range(0f, 1f)]public float probabilityFactor = 0.6f;
    float timeToSpawn = 30f; //in seconds

    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        int n = Random.Range(0, powerUps.Length);
        timer += Time.deltaTime;
        if (timer >= timeToSpawn)
        {
            if (powerUps[n] != null)
            {
                if (Random.value <= probabilityFactor)
                {
                    Instantiate(powerUps[n].gameObject);
                }
            }
            timer = 0f;
        }
    }
}
