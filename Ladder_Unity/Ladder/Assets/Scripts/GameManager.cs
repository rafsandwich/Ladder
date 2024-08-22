using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        // debugging to ensure moves are intact
        Debug.Log($"User Lad {userLad.ladName} has {userLad.moves.Count} moves");
        Debug.Log($"Computer Lad {computerLad.ladName} has {computerLad.moves.Count} moves");

        // assigning roles to lads
        //userLad.Initialise(userLad.ladName, userLad.hp, userLad.spd, userLad.moves, "Player");
        //computerLad.Initialise(computerLad.ladName, computerLad.hp, computerLad.spd, computerLad.moves, "Opponent");

        userLad.role = "Player";
        computerLad.role = "Opponent";

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
            // 1. Always start by letting the player choose their move
            yield return StartCoroutine(PlayerTurn(lad1, lad2));

            // 2. If the player's lad is dead after the enemy's turn, end the battle
            if (lad1.IsDead())
            {
                uiManager.ShowGameOver(lad2.ladName + " (" + lad2.role + ") wins!");
                Debug.Log(lad2.ladName + " (" + lad2.role + ") wins!");
                yield break;
            }

            // 3. Determine the move order based on speed
            if (lad1.spd >= lad2.spd)
            {
                // Player's Lad outspeeds so moves first
                lad1.Attack(lad2, playerChosenMove);
                if (!lad2.IsDead())
                {
                    yield return EnemyTurn(lad2, lad1);
                }
            }
            else
            {
                // Enemy's Lad outspeeds so moves first
                yield return EnemyTurn(lad2, lad1);
                if (!lad1.IsDead())
                {
                    lad1.Attack(lad2, playerChosenMove);
                }
            }

            uiManager.UpdateHPBars();

            // 4. If either lad is dead after all moves are done, end the battle
            if (lad1.IsDead())
            {
                uiManager.ShowGameOver(lad2.ladName + " (" + lad2.role + ") wins!");
                Debug.Log(lad2.ladName + " (" + lad2.role + ") wins!");
                yield break;
            }
            else if (lad2.IsDead())
            {
                uiManager.ShowGameOver(lad1.ladName + " (" + lad1.role + ") wins!");
                Debug.Log(lad1.ladName + " (" + lad1.role + ") wins!");
                yield break;
            }
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
            playerChosenMove = uiManager.move1Button.GetComponentInChildren<Text>().text;
            playerMoveChosen = true;
        });

        uiManager.move2Button.onClick.AddListener(() => {
            playerChosenMove = uiManager.move2Button.GetComponentInChildren<Text>().text;
            playerMoveChosen = true;
        });

        while (!playerMoveChosen)
        {
            yield return null;
        }

        //player.Attack(enemy, playerChosenMove);

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
        //string move = Random.Range(0, 2) == 0 ? "Move1" : "Move2";
        string move = enemy.moves[Random.Range(0, enemy.moves.Count)].moveName;
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
        { Debug.Log("where is selection script!!"); } // we found it!

    }
}
