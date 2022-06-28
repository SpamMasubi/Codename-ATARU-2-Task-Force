using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public static GameStats instance;
    public int numOfMissiles = 12;
    public Text missileText;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(missileText != null)
        {
            missileText.text = numOfMissiles.ToString();
        }
    }
    public bool checkCanShootMissile(int amount)
    {
        bool p = false;
        if(numOfMissiles - amount >= 0)
        {
            p = true;
        }

        return p;
    }
    public void shootMissileByAmount(int amount)
    {
        if(numOfMissiles - amount >= 0)
        {
            numOfMissiles -= amount;
        }
    }

    public void addMissileByAmount(int amount)
    {
        if (amount >= 0)
        {
            numOfMissiles += amount;
        }
    }
}
