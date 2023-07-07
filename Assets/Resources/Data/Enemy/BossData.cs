using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Data/Boss")]
public class BossData : EnemyData
{
    public string bossName;
    public int Skill1Damage;
    public int Skill2Damage;
}
