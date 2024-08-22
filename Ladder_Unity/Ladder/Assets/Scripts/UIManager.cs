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
        //move1Button.onClick.AddListener(() => OnMoveButtonClicked("Move1"));
        //move2Button.onClick.AddListener(() => OnMoveButtonClicked("Move2"));

        move1Button.onClick.AddListener(() => OnMoveButtonClicked(lad1.moves[0].moveName));
        move2Button.onClick.AddListener(() => OnMoveButtonClicked(lad1.moves[1].moveName));

        UpdateMoveButtons(lad1.moves);
    }

    public void UpdateMoveButtons(List<Move> moves)
    {
        Debug.Log("Updating Move Buttons");
        Debug.Log($"Number of moves: {moves.Count}");

        if (moves.Count > 0)
        {
            Debug.Log($"Move1: {moves[0].moveName}");
        }

        if (moves.Count > 1)
        {
            Debug.Log($"Move2: {moves[1].moveName}");
        }

        if (move1Button != null && move1Button.GetComponentInChildren<Text>() != null)
        {
            if (moves.Count > 0)
            {
                move1Button.GetComponentInChildren<Text>().text = moves[0].moveName;
            }
        }
        else
        {
            Debug.LogError("Move1 button text component is missing or no btn");
        }

        if (move2Button != null && move2Button.GetComponentInChildren<Text>() != null)
        {
            if (moves.Count > 1)
            {
                move2Button.GetComponentInChildren<Text>().text = moves[1].moveName;
            }
        }
        else
        {
            Debug.LogError("Move2 button text component is missing or no btn");
        }
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
        Debug.Log("Resetting UI");
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        Debug.Log("Resetting Move Buttons");

        if (move1Button != null && move1Button.GetComponentInChildren<Text>() != null)
        {
            move1Button.GetComponentInChildren<Text>().text = "Move1";
        }
        else
        {
            Debug.LogError("Move1 button text component is missing or no btn");
        }

        if (move2Button != null && move2Button.GetComponentInChildren<Text>() != null)
        {
            move2Button.GetComponentInChildren<Text>().text = "Move2";
        }
        else
        {
            Debug.LogError("Move2 button text component is missing or no btn");
        }
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