using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[Serializable]
public class Move
{
    public string moveName;
    public int damage;
}

public class Lad : MonoBehaviour
{
    public string ladName;
    public int hp;
    public int spd;
    //public Dictionary<string, int> moves = new Dictionary<string, int>();
    public List<Move> moves = new List<Move>();
    public string role;

    public void Initialise(string name, int health, int speed, List<Move> moveList, string role)
    {
        ladName = name;
        hp = health;
        spd = speed;
        moves.Clear();
        //moves.Add("Move1", move1);
        //moves.Add("Move2", move2);

        moves = moveList;

        Debug.Log($"Initializing Lad: {ladName} with {moves.Count} moves and role {role}");


        this.role = role;
    }

    public void Attack(Lad other, string move)
    {
        Move selectedMove = moves.Find(m => m.moveName == move);
        if (selectedMove != null)
        {
            int damage = selectedMove.damage;
            other.hp -= damage;

            Debug.Log($"{ladName} used {move} on {other.ladName}, dealing {damage} damage!");
            Debug.Log($"{other.ladName} | HP = {other.hp}");
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
