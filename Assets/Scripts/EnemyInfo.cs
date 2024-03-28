using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyInfo", menuName = "EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public new string name;
    public int health;
    public int score;
    public GameObject bullet;
    public GameObject exp;
}
    