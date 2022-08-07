using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Epilogue : MonoBehaviour
{
    public Sprite[] characters;
    public SpriteRenderer character;
    public Text dialogue1, dialogue2, dialogue3;
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.stage)
        {
            case 2:
                character.sprite = characters[0];
                break;
            case 3:
                character.flipX = true;
                character.sprite = characters[1];
                break;
            case 4:
                dialogue1.text = "GRRAGH! ATARU, I will never forgive you for killing my brothers! You have broken the last straw of mine! I will take you on my own!";
                dialogue2.text = "I, Commander Maru Sanada of the Red Giant, will make sure this world will pay for their treachery! The death of my brothers will not go in vain!";
                dialogue3.text = "Comrades! Our final battle will begin! Attack every UN Forces on earth! No mercy! For the glory of the Red Giant!";
                break;
            default:
                break;
        }
    }

}
