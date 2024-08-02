using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lad : MonoBehaviour
{
    public string ladName;
    public int hp;
    public int spd;
    public Dictionary<string, int> moves = new Dictionary<string, int>();

    public void Initialize(string name, int health, int speed, int move1, int move2)
    {
        ladName = name;
        hp = health;
        spd = speed;
        moves.Add("Move1", move1);
        moves.Add("Move2", move2);
    }

    public void Attack(Lad other, string move)
    {
        if (moves.ContainsKey(move))
        {
            int damage = moves[move];
            other.hp -= damage;

            Debug.Log($"{ladName} used {move} on {other.ladName}, dealing {damage} damage!");
            Debug.Log($"{other.ladName} | HP is now {other.hp}");
        }
        else
        {
            Debug.Log("Invalid move!");
        }
    }

    public bool IsDead()
    {
        return hp <= 0;
    }
}
