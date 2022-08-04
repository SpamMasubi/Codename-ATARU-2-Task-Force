using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    void OnEnable()
    {
        /*
        if (FinalBoss.gameComplete)
        {
            sceneName = "Credit Scene";
            FinalBoss.gameComplete = false;
        }*/
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
