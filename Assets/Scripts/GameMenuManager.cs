using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager instance;
    public enum PlayerMovementInputType
    {
        MouseControl, ButtonControl, TiltControl
    }
    private string playerMovementTypeKey = "PMIT";
    private PlayerMovementInputType _pp;
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
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void switchToTilt()
    {
        _pp = PlayerMovementInputType.TiltControl;
        PlayerPrefs.SetInt(playerMovementTypeKey, 2);
    }
    public void switchToButton()
    {
        _pp = PlayerMovementInputType.ButtonControl;
        PlayerPrefs.SetInt(playerMovementTypeKey, 1);
    }

    public void switchToMouse()
    {
        _pp = PlayerMovementInputType.MouseControl;
        PlayerPrefs.SetInt(playerMovementTypeKey, 0);
    }
}
