using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : BaseEnemy
{
    [SerializeField] Transform attackPoint;
    Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        enemyData = GameManager.Resource.Load<EnemyData>("Data/Enemy/Slasher");
    }
    protected override void Start()
    {
        base.Start();
        // SetStartPosition(7, 0);
    }
    protected override void Update()
    {
        base.Update();
    }
    public void CreatAttack()
    {
        GameManager.Resource.Instantiate<SlasherShotWide>("Effect/Enemy/SlasherShotWide", attackPoint.position, attackPoint.rotation);
        StartCoroutine(attackRoutineData.ProjectileAttack(xPos, yPos, 3, atk, 0.15f, mask));
    }
    
    protected override void AttackState()
    {
        animator.SetTrigger("Attack");
        SetState(State.Idle);
    }

    protected override void FindState()
    {
        
    }

    protected override void HitState()
    {
        animator.SetTrigger("Hit");
        if(curHp <= 0)
            SetState(State.Dead);
        else SetState(State.Idle);
    }

    protected override void IdleState()
    {
        ap += speed * Time.deltaTime;
        if(ap >= 100)
        {
            ap = 0;
            SetState((State)Random.Range(2, 4));
        }
    }

    protected override void MoveState()
    {
        if (GameManager.Data.playerYPos > yPos)
        {
            SetPosition(xPos, yPos + 1);
        }
        else if (GameManager.Data.playerYPos < yPos)
        {
            SetPosition(xPos, yPos - 1);
        }
        else
            SetState(State.Attack);
        
        SetState(State.Idle);
    }
    protected override void DeadState()
    {
        GameManager.Resource.Destroy(gameObject);
    }

    
    
}
