using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    protected AttackRoutineData atkData;
    protected LayerMask playerAtkMask;
    protected LayerMask enemyAtkMask;
    protected virtual void Awake()
    {
        atkData = GameManager.Resource.Load<AttackRoutineData>("Data/AttackRoutineData");
        playerAtkMask = LayerMask.GetMask("Enemy", "Envi", "Npc", "Obstacle");
        enemyAtkMask = LayerMask.GetMask("Player", "Npc", "Obstacle");
    }
}
