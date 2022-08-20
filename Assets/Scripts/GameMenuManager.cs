using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager instance;
    private AudioSource music;
    public static float musicVolumeSet;
    public static float sfxVolumeSet;
    public AudioClip selection;
    public Slider volumeSlider;
    public Slider sfxSlider;

    //StandAlone
    public GameObject controlMenu, mainMenu;
    public GameObject startGameButton, controlButton, controlBackButton;

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
        music = GetComponent<AudioSource>();
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey);
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(startGameButton);
    }

    private void Update()
    {
        music.volume = volumeSlider.value;
        musicVolumeSet = music.volume;

        sfxVolumeSet = sfxSlider.value;
    }

    public void controlOpen()
    {

        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(controlBackButton);

    }

    public void controlClose()
    {
        controlMenu.SetActive(false);
        mainMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(controlButton);
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

    public void creditHS()
    {
        if (!hasSelected)
        {
            hasSelected = true;
            playSound();
            StartCoroutine(LoadScene(9));
        }
    }

    public IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
        hasSelected = false;
    }

    public void savePrefs()
    {
        playSound();
        PlayerPrefs.SetFloat("MusicVolume", music.volume);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
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
        GameSoundManager.instance.playSFX(selection);
    }
}
