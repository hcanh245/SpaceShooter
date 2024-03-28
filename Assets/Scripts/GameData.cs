using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;
    public string map;
    public int turn;
    public int health;
    public Vector2 pos;
    public float speed;
    public int bulletLevel;

    public GameData()
    {
        score = 0;
        map = "1";
        turn = 1;
        health = 3;
        pos = new Vector2(0, -4.5f);
        speed = 3.5f;
        bulletLevel = 0;
    }
}
