using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AttackBase
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        transform.position = GameManager.Data.map[GameManager.Data.playerXPos + 4, GameManager.Data.playerYPos];
        atkData.TargetAttack(GameManager.Data.playerXPos + 4, GameManager.Data.playerYPos, 100, playerAtkMask, out bool b);
        GameManager.Resource.Destroy(gameObject, 2f);
    }
}
