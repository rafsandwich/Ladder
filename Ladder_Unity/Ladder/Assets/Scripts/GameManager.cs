using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Lad lad1;
    public Lad lad2;
    public UIManager uiManager;

    private string playerChosenMove = "";
    private bool playerMoveChosen = false;

    void Start()
    {
        // name, health, speed, move1 dmg, move2 dmg

        lad1.Initialize("Fish", 15, 10, 13, 6);
        lad2.Initialize("Kafka", 22, 5, 8, 11);

        // lad2.Initialize("Seagull", 17, 9, 12, 9);
        // lad2.Initialize("Dirt", 35, 2, 5, 10);
        // lad2.Initialize("Bed", 40, 1, 4, 7);
        // lad2.Initialize("Ypoch", 23, 8, 6, 12);

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
            Debug.Log(lad2.ladName + " wins!");
        }
        else if (lad2.IsDead())
        {
            Debug.Log(lad1.ladName + " wins!");
        }
    }

    private IEnumerator PlayerTurn(Lad player, Lad enemy)
    {
        playerMoveChosen = false;

        // Enable move buttons
        uiManager.move1Button.interactable = true;
        uiManager.move2Button.interactable = true;

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
}
