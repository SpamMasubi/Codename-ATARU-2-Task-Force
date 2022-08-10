using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterIntro : MonoBehaviour
{
    public Text stageTitle, chapterName;
    public AudioClip selection;
    private int stage;
    public GameObject startButton;
    private bool canStart;
    private bool hasSelected;

    // Start is called before the first frame update
    void Start()
    {
        stage = GameManager.stage;
        switch (GameManager.stage)
        {
            case 1:
                stageTitle.text = "Stage 1";
                chapterName.text = "The Return of ATARU";
                break;
            case 2:
                stageTitle.text = "Stage 2";
                chapterName.text = "Revenge of the Red Giant";
                break;
            case 3:
                stageTitle.text = "Stage 3";
                chapterName.text = "The Road to Victory";
                break;
            case 4:
                stageTitle.text = "Final Stage";
                chapterName.text = "The Final War: United Nation Task Force vs Red Giant";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(canStartGame());
        if (canStart && !hasSelected)
        {
            if(startButton != null)
            {
                startButton.SetActive(true);
            }
            else if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
            {
                playSound();
                StartCoroutine(NextStage(GameManager.stage + 2));
            }
        }
    }

    public void startStage()
    {
        if (!hasSelected)
        {
            playSound();
            StartCoroutine(NextStage(GameManager.stage + 2));
        }
    }

    IEnumerator canStartGame()
    {
        yield return new WaitForSeconds(5f);
        canStart = true;
    }

    public IEnumerator NextStage(int loadStage)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(loadStage);
        canStart = false;
    }

    public void playSound()
    {
        GameSoundManager.instance.playSFX(selection);
    }
}
