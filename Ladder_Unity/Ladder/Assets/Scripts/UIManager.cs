using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Lad lad1;
    public Lad lad2;

    public Slider lad1HPBar;
    public Slider lad2HPBar;
    public TextMeshProUGUI lad1NameText;
    public TextMeshProUGUI lad1HPText;
    public TextMeshProUGUI lad2NameText;
    public TextMeshProUGUI lad2HPText;
    public Button move1Button;
    public Button move2Button;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    //public Image lad1Image;
    //public Image lad2Image;
    // public Sprite lad1Sprite;
    //public Sprite[] ladSprites;

    void Start()
    {
        restartButton.onClick.AddListener(() => FindObjectOfType<GameManager>().ReturnToSelectionScreen());
        ResetUI();
    }

    public void InitialiseUI()
    {
        // Initialise UI with lad info
        lad1NameText.text = lad1.ladName;
        lad1HPBar.maxValue = lad1.hp;
        lad1HPBar.value = lad1.hp;
        lad1HPText.text = lad1.hp.ToString();

        lad2NameText.text = lad2.ladName;
        lad2HPBar.maxValue = lad2.hp;
        lad2HPBar.value = lad2.hp;
        lad2HPText.text = lad2.hp.ToString();

        // Set lad images 
        //lad1Image.sprite = lad1Sprite;

        //lad1Image.sprite = GetLadSprite(lad1.ladName);
        //lad2Image.sprite = GetLadSprite(lad2.ladName);

        // Add listeners to move buttons
        move1Button.onClick.AddListener(() => OnMoveButtonClicked("Move1"));
        move2Button.onClick.AddListener(() => OnMoveButtonClicked("Move2"));
    }

    public void UpdateHPBars()
    {
        if (lad1.hp > 0)
        {
            lad1HPBar.value = lad1.hp;
            lad1HPText.text = lad1.hp.ToString();
        }
        else
        {
            lad1HPBar.value = 0;
            lad1HPText.text = "0";
        }
        if (lad2.hp > 0)
        {
            lad2HPBar.value = lad2.hp;
            lad2HPText.text = lad2.hp.ToString();
        }
        else
        {
            lad2HPBar.value = 0;
            lad2HPText.text = "0";
        }
    }

    public void ShowGameOver(string message)
    {
        gameOverText.text = message;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void ResetUI()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    private void OnMoveButtonClicked(string move)
    {
        FindObjectOfType<GameManager>().PlayerMoveSelected(move);
    }
    //private Sprite GetLadSprite(string ladName)
    //{
    //    foreach (Sprite sprite in ladSprites)
    //    {
    //        if (sprite.name == ladName)
    //        {
    //            return sprite;
    //        }
    //    }
    //    return null;
    //}
}