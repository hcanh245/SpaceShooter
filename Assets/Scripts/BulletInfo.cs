using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletInfo", menuName = "BulletInfo")]
public class BulletInfo : ScriptableObject
{
    public new string name;
    public float speed;
    public int dame;
}
