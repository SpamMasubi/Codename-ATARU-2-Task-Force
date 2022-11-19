using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class selectStage : MonoBehaviour
{
    public AudioClip selection;
    public GameObject backButton;

    public static bool replay;

    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        replay = true;
        GameManager.stage = 1;
#if UNITY_STANDALONE || UNITY_WEBGL
        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(backButton);
#endif
    }
    public void selectScene(int sceneNumber)
    {
        if (!isClicked)
        {
            playSound(selection);
            isClicked = true;
            switch (sceneNumber)
            {
                case 4:
                    GameManager.stage = 2;
                    break;
                case 5:
                    GameManager.stage = 3;
                    break;
                case 6:
                    GameManager.stage = 4;
                    break;
                default:
                    break;
            }
            StartCoroutine(LoadScene(sceneNumber));
        }
    }
    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(index);
        isClicked = false;
    }

    public void playSound(AudioClip audioclip)
    {
        GameSoundManager.instance.playSFX(audioclip);
    }

    public void deleteGameManager()
    {
        replay = false;
        Destroy(FindObjectOfType<GameManager>().gameObject);
    }
}
