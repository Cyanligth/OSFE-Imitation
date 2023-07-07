using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slash2x3 : AttackBase
{
    PlayerMover mover;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        transform.position = GameManager.Data.map[GameManager.Data.playerXPos + 3, GameManager.Data.playerYPos] + new Vector2(0, 1);
        mover = GameObject.Find("Player").GetComponent<PlayerMover>();
        StartCoroutine(Routine());
    }
    IEnumerator Routine()
    {
        GameManager.Player.anchor = true;
        int x = GameManager.Data.playerXPos;
        int y = GameManager.Data.playerYPos;
        StartCoroutine(mover.MoveCorutine(mover.gameObject.transform.position, GameManager.Data.map[x+3, y], 10));
        
        for (int i = 4; i < 6; i++)
        {
            for(int j = -1; j < 2; j++)
            {
                if(x + i > -1 && x + i < 8 && y + j > -1 && y + j < 4)
                    atkData.TargetAttack(x + i, y + j, 80, playerAtkMask, out bool b);
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(mover.MoveCorutine(mover.gameObject.transform.position, GameManager.Data.map[x, y], 10));
        GameManager.Player.anchor = false;
    }
}
