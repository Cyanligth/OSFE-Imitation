using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int hp;
    public int speed;
    public int atk;
}
