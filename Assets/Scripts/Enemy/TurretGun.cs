using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : BaseEnemy
{
    Animator animator;
    [SerializeField]Transform atkPos;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        enemyData = GameManager.Resource.Load<EnemyData>("Data/Enemy/TurretGun");
    }
    protected override void Start()
    {
        base.Start();
        // SetStartPosition(5,1);
    }
    protected override void Update()
    {
        base.Update();
    }
    public void Attack()
    {
        RedBall ball = GameManager.Resource.Instantiate<RedBall>("Effect/Enemy/RedBall", atkPos.position, atkPos.rotation);
        StartCoroutine(attackRoutineData.ProjectileAttack(xPos, yPos, atk, 0.08f, mask, false));
    }
    protected override void AttackState()
    {
        animator.SetTrigger("Shot");
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
        if(ap >= 100)
        {
            ap = 0;
            SetState(State.Attack);
        }
    }

    protected override void MoveState(){ }
}
