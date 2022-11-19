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
    public static float tiltValue;
    public AudioClip selection;
    public Toggle fullScreenToggle;
    
    //StandAlone
    public GameObject controlMenu, mainMenu;
    public GameObject startGameButton, controlButton, controlBackButton;    
    public Slider volumeSlider;
    public Slider sfxSlider;    
    public Button replay;
    public GameObject DataDeleteText;
    
    ///WebGL
    public GameObject controlMenuWGL, mainMenuWGL;
    public GameObject startGameButtonWGL, controlButtonWGL, controlBackButtonWGL;
    public Slider volumeSliderWGL;
    public Slider sfxSliderWGL;
    public Button replayWGL;
    public GameObject DataDeleteTextWGL;

    //Mobile
    public GameObject controlMenuMobile, mainMenuMobile;
    public Slider volumeSliderMobile;
    public Slider sfxSliderMobile;
    public Slider tiltSlider;
    public Text tiltValueText;
    public Button replayMobile;
    public GameObject DataDeleteTextMobile;

    //Main Menu base on platforms
    public GameObject StandAloneMenu, WebGLMenu, MobileMenu; //1.StandAlone Platform 2.WebGL 3.Mobile Devices

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
#if UNITY_STANDALONE
        StandAloneMenu.SetActive(true);        
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(startGameButton);
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        replay.interactable = (PlayerPrefs.GetInt("ReplayButtonActive") == 1) ? true : false;
        fullScreenToggle.isOn = (PlayerPrefs.GetInt("FullScreenOn", 1) == 1) ? true : false;
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey);
#elif UNITY_WEBGL
        WebGLMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(startGameButtonWGL);
        volumeSliderWGL.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSliderWGL.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        replayWGL.interactable = (PlayerPrefs.GetInt("ReplayButtonActive") == 1) ? true : false;
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey);
#elif UNITY_ANDROID || UNITY_IOS
        MobileMenu.SetActive(true);
        volumeSliderMobile.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSliderMobile.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        tiltSlider.value = PlayerPrefs.GetFloat("TiltValue", 100f);
        replayMobile.interactable = (PlayerPrefs.GetInt("ReplayButtonActive") == 1) ? true : false;
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey, 1);
#endif
        music = GetComponent<AudioSource>();
    }

    private void Update()
    {
#if UNITY_STANDALONE
        music.volume = volumeSlider.value;
        sfxVolumeSet = sfxSlider.value;
#elif UNITY_WEBGL
        music.volume = volumeSliderWGL.value;
        sfxVolumeSet = sfxSliderWGL.value;
#elif UNITY_IOS || UNITY_ANDROID
        music.volume = volumeSliderMobile.value;
        sfxVolumeSet = sfxSliderMobile.value;
        tiltValue = tiltSlider.value;
        if(tiltValueText != null)
        {
            tiltValueText.text = tiltValue.ToString();
        }
#endif
        musicVolumeSet = music.volume;
    }

    public void controlOpen()
    {
#if UNITY_STANDALONE
        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(controlBackButton);
#elif UNITY_WEBGL
        controlMenuWGL.SetActive(true);
        mainMenuWGL.SetActive(false);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(controlBackButtonWGL);
#elif UNITY_IOS || UNITY_ANDROID
        controlMenuMobile.SetActive(true);
        mainMenuMobile.SetActive(false);
#endif
    }

    public void controlClose()
    {
#if UNITY_STANDALONE
        controlMenu.SetActive(false);
        mainMenu.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(controlButton);
#elif UNITY_WEBGL
        controlMenuWGL.SetActive(false);
        mainMenuWGL.SetActive(true);
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(controlButtonWGL);
#elif UNITY_IOS || UNITY_ANDROID
        controlMenuMobile.SetActive(false);
        mainMenuMobile.SetActive(true);
#endif
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

    public void replayGame()
    {
        if (!hasSelected)
        {
            hasSelected = true;
            playSound();
            StartCoroutine(LoadScene(10));
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
#if UNITY_STANDALONE
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        saveToggleScreen(fullScreenToggle.isOn);
        setFullScreen(fullScreenToggle.isOn);
#elif UNITY_WEBGL
        PlayerPrefs.SetFloat("SFXVolume", sfxSliderWGL.value);
#elif UNITY_ANDROID || UNITY_IOS
        PlayerPrefs.SetFloat("SFXVolume", sfxSliderMobile.value);
        PlayerPrefs.SetFloat("TiltValue", tiltSlider.value);
#endif
    }

    public void resetPlayerPrefs()
    {
        playSound();
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("SFXVolume");
        PlayerPrefs.DeleteKey(playerMovementTypeKey);
        PlayerPrefs.DeleteKey("FullScreenOn");
        PlayerPrefs.DeleteKey("TiltValue");
#if UNITY_STANDALONE
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        fullScreenToggle.isOn = (PlayerPrefs.GetInt("FullScreenOn", 1) == 1) ? true : false;
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey);
#elif UNITY_WEBGL
        volumeSliderWGL.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSliderWGL.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey);
#elif UNITY_ANDROID || UNITY_IOS
        volumeSliderMobile.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSliderMobile.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        tiltSlider.value = PlayerPrefs.GetFloat("TiltValue", 100f);
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovementTypeKey, 1);
#endif
    }

    public void deleteData()
    {
        playSound();
        PlayerPrefs.DeleteKey("Your Score");
        PlayerPrefs.DeleteKey("High Score");
        PlayerPrefs.DeleteKey("ReplayButtonActive");
        replayWGL.interactable = (PlayerPrefs.GetInt("ReplayButtonActive") == 1) ? true : false;
        PlayerPrefs.GetInt("Your Score");
        PlayerPrefs.GetInt("High Score");
#if UNITY_STANDALONE
        DataDeleteText.SetActive(true);
#elif UNITY_WEBGL
        DataDeleteTextWGL.SetActive(true);
#elif UNITY_ANDROID || UNITY_IOS
        DataDeleteTextMobile.SetActive(true);
#endif
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(3f);
#if UNITY_STANDALONE
        DataDeleteText.SetActive(false);
#elif UNITY_WEBGL
        DataDeleteTextWGL.SetActive(false);
#endif
    }

    void saveToggleScreen(bool toggle)
    {
        if (toggle)
        {
            PlayerPrefs.SetInt("FullScreenOn", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreenOn", 0);
        }
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

    public void setFullScreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        else
        {
            Screen.SetResolution(960, 540, false);
        }
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }
}
