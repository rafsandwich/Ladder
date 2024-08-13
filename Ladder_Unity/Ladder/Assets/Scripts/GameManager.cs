using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Lad lad1;
    private Lad lad2;
    public UIManager uiManager;

    private string playerChosenMove = "";
    private bool playerMoveChosen = false;

    //void Start()
    //{
    //    // name, health, speed, move1 dmg, move2 dmg

    //    lad1.Initialise("Fish", 15, 10, 13, 6);
    //    lad2.Initialise("Kafka", 22, 5, 8, 11);

    //    // lad2.Initialise("Seagull", 17, 9, 12, 9);
    //    // lad2.Initialise("Dirt", 35, 2, 5, 10);
    //    // lad2.Initialise("Bed", 40, 1, 4, 7);
    //    // lad2.Initialise("Ypoch", 23, 8, 6, 12);

    //    uiManager.UpdateHPBars();
    //    StartCoroutine(Battle());
    //}

    public void InitialiseBattle(Lad userLad, Lad computerLad)
    {
        lad1 = userLad;
        lad2 = computerLad;

        uiManager.ResetUI();

        uiManager.lad1 = lad1;
        uiManager.lad2 = lad2;

        uiManager.InitialiseUI();
        uiManager.UpdateHPBars();

        StartCoroutine(Battle());
    }

    private IEnumerator Battle()
    {
        while (lad1.hp > 0 && lad2.hp > 0)
        {
            if (lad1.spd > lad2.spd)
            {
                yield return PlayerTurn(lad1, lad2);
                if (!lad2.IsDead())
                {
                    yield return EnemyTurn(lad2, lad1);
                }
            }
            else
            {
                yield return EnemyTurn(lad2, lad1);
                if (!lad1.IsDead())
                {
                    yield return PlayerTurn(lad1, lad2);
                }
            }

            uiManager.UpdateHPBars();
        }

        if (lad1.IsDead())
        {
            uiManager.ShowGameOver(lad2.ladName + " wins!");
            Debug.Log(lad2.ladName + " wins!");
        }
        else if (lad2.IsDead())
        {
            uiManager.ShowGameOver(lad1.ladName + " wins!");
            Debug.Log(lad1.ladName + " wins!");
        }
    }

    private IEnumerator PlayerTurn(Lad player, Lad enemy)
    {
        bool playerMoveChosen = false;
        string playerChosenMove = "";

        // Enable move buttons
        uiManager.move1Button.interactable = true;
        uiManager.move2Button.interactable = true;

        uiManager.move1Button.onClick.AddListener(() => {
            playerChosenMove = "Move1";
            playerMoveChosen = true;
        });

        uiManager.move2Button.onClick.AddListener(() => {
            playerChosenMove = "Move2";
            playerMoveChosen = true;
        });

        while (!playerMoveChosen)
        {
            yield return null;
        }

        player.Attack(enemy, playerChosenMove);

        // Disable move buttons
        uiManager.move1Button.interactable = false;
        uiManager.move2Button.interactable = false;
    }

    public void PlayerMoveSelected(string move)
    {
        playerChosenMove = move;
        playerMoveChosen = true;
    }

    private IEnumerator EnemyTurn(Lad enemy, Lad player)
    {
        string move = Random.Range(0, 2) == 0 ? "Move1" : "Move2";
        enemy.Attack(player, move);
        yield return new WaitForSeconds(1);  // wait 1 second to simulate delay
    }

    public void ReturnToSelectionScreen()
    {
        Debug.Log("Destroying Lad1");
        Destroy(lad1.gameObject);

        Debug.Log("Destroying Lad2");
        Destroy(lad2.gameObject);

        //Debug.Log("Resetting Selection");
        //FindObjectOfType<SelectionScript>().ResetSelection();    

        SelectionScript selectionScript = FindObjectOfType<SelectionScript>();
        if (selectionScript != null)
        {
            Debug.Log("Resetting Selection");
            selectionScript.ResetSelection();
        }
        else
        { Debug.Log("where is selection script!!"); } // TO FIX WHERE IS IT?? HELLO

    }
}
