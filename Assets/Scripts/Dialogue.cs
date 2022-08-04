using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Sprite[] mugshot;
    public AudioClip[] voices;
    public Image mugshotTemp;
    public Text dialogue;

    private Animator anim;
    private AudioSource audioS;
    public 

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        switch (GameManager.stage)
        {
            case 1:
                mugshotTemp.sprite = mugshot[0];
                break;
            case 2:
                mugshotTemp.sprite = mugshot[1];
                dialogue.text = "Colonel Winchester \n\n" +
                    "ATARU! I will crush you and your UN Task Force! No one can defeat me, Colonel Winchester! I shall bring victory to the Red Giant. For you, my brothers!";
                break;
            case 3:
                mugshotTemp.sprite = mugshot[2];
                dialogue.text = "Admiral Jurgensen \n\n" +
                    "You cannot escape me, ATARU! The seas are my strength and my ship will sink your jet out of the sky! My brothers, the Red Giant shall rule this world!";
                break;
            case 4:
                mugshotTemp.sprite = mugshot[3];
                dialogue.text = "Commander Sanada \n\n" +
                    "The new world with the Red Giant as victorious! The United Nation will be defeated by us! The death of my brothers shall never be in vain. ATARU! DIE, FOR THE RED GIANT'S GLORY!!!";
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        StartCoroutine(waitTilDisable());
    }

    public void disableDialogue()
    {
        this.gameObject.SetActive(false);
        anim.SetBool("FinishDialogue", false);
    }

    private IEnumerator waitTilDisable()
    {
        yield return new WaitForSeconds(10f);
        anim.SetBool("FinishDialogue", true);
    }

    public void playVoice()
    {
        switch (GameManager.stage)
        {
            case 1:
                voice(voices[0]);
                break;
            case 2:
                voice(voices[1]);
                break;
            case 3:
                voice(voices[2]);
                break;
            case 4:
                voice(voices[3]);
                break;
            default:
                break;
        }
    }

    private void voice(AudioClip voice)
    {
        audioS.PlayOneShot(voice, 1f);
    }
}
