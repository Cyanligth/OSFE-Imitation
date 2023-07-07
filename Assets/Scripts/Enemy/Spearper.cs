using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spearper : BaseEnemy
{
    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        enemyData = GameManager.Resource.Load<EnemyData>("Data/Enemy/Spearper");
    }
    protected override void Start()
    {
        base.Start();
        // SetStartPosition(5,2);
    }
    protected override void Update()
    {
        base.Update();
    }
    public void AttackMoveRush()
    {
        SetTemporaryPos(GameManager.Data.playerXPos + 1, yPos);
    }
    public void AttackMoveBack()
    {
        SetTemporaryPos(6, yPos);
    }
    public void AttackEff()
    {
        GameManager.Resource.Instantiate<SpearSlash>("Effect/Enemy/SpearSlash", transform.position + new Vector3(0, 1), transform.rotation);
        attackRoutineData.TargetAttack(xPos - 1, yPos, enemyData.atk, mask, out bool b);
        attackRoutineData.TargetAttack(xPos - 2, yPos, enemyData.atk, mask, out b);
    }

    protected override void AttackState()
    {
        animator.SetTrigger("Attack");
        SetState(State.Idle);
    }
    protected override void DeadState()
    {
        GameManager.Resource.Destroy(gameObject);
    }
    protected override void FindState(){}
    protected override void HitState()
    {
        animator.SetTrigger("Hit");
        if (curHp <= 0)
            SetState(State.Dead);
        else SetState(State.Idle);
    }
    protected override void IdleState()
    {
        ap += speed * Time.deltaTime;
        if (ap >= 100)
        {
            ap = 0;
            SetState(State.Move);
        }
    }
    protected override void MoveState()
    {
        if(yPos == GameManager.Data.playerYPos)
        {
            SetState(State.Attack);
            return;
        }
        int r = Random.Range(0, 4);
        switch(r)
        {
            case 0:
            case 1:
                if (GameManager.Data.playerYPos > yPos)
                {
                    SetPosition(xPos, yPos + 1);
                }
                else if (GameManager.Data.playerYPos < yPos)
                {
                    SetPosition(xPos, yPos - 1);
                }
                break; 
            case 2:
                SetPosition(xPos + 1, yPos);
                break; 
            case 3:
                SetPosition(xPos - 1, yPos);
                break; 
        }
        SetState(State.Idle);
    }
}
