using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SkipScene : MonoBehaviour
{
    public string sceneName;
    private bool isClicked;
    public AudioClip selection;

    public GameObject skipButton;

    private void Start()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(skipButton);
#endif
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && skipButton == null)
        {
            skipScene();
        }
    }

    public void skipScene()
    {
        if (!isClicked)
        {
            playSound();
            isClicked = true;
            Invoke("LoadNextScene", 1f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        isClicked = false;
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }
}
