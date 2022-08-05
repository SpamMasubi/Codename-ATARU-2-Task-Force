using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager instance;
    public AudioClip selection;
    public enum PlayerMovementInputType
    {
        MouseControl, ButtonControl, TiltControl
    }
    private string playerMovementTypeKey = "PMIT";
    private PlayerMovementInputType _pp;
    private bool hasSelected;
    public PlayerMovementInputType currentPMIT { get { return _pp; } }
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey);

    }

    public void QuitGame()
    {
        playSound();
        Application.Quit();
    }

    public void startGame()
    {
        if (!hasSelected)
        {
            hasSelected = true;
            playSound();
            StartCoroutine(LoadScene(2));
        }
    }

    public IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
        hasSelected = false;
    }

    public void switchToTilt()
    {
        playSound();
        _pp = PlayerMovementInputType.TiltControl;
        PlayerPrefs.SetInt(playerMovementTypeKey, 2);
    }
    public void switchToButton()
    {
        playSound();
        _pp = PlayerMovementInputType.ButtonControl;
        PlayerPrefs.SetInt(playerMovementTypeKey, 1);
    }

    public void switchToMouse()
    {
        playSound();
        _pp = PlayerMovementInputType.MouseControl;
        PlayerPrefs.SetInt(playerMovementTypeKey, 0);
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection, 1f);
    }
}
