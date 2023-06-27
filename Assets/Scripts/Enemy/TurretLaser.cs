using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : BaseEnemy
{
    [SerializeField] Transform attackPoint;
    Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        enemyData = GameManager.Resource.Load<EnemyData>("Data/Enemy/TurretLaser");
    }
    protected override void Start()
    {
        base.Start();
        SetPosition(6, 3);
    }
    protected override void Update()
    {
        base.Update();
    }
    public void OnAttack()
    {
        GameManager.Resource.Instantiate<Laser>("Effect/Enemy/BeamLaser", attackPoint.position, attackPoint.rotation);
        attackRoutineData.BeamAttack(xPos, yPos, atk, LayerMask.GetMask("Player"));
    }

    protected override void AttackState()
    {
        if(ap > 200)
        {
            ap = 0;
            animator.SetTrigger("Shot");
            SetState(State.Idle);
        }
        ap += speed * Time.deltaTime;
        animator.SetFloat("Speed", ap);

    }

    protected override void DeadState()
    {
        GameManager.Resource.Destroy(gameObject);
    }

    protected override void FindState(){ }

    protected override void HitState()
    {
        animator.SetTrigger("Hit");
        if (curHp <= 0)
            SetState(State.Dead);
        else
            SetState(State.Idle);
    }

    protected override void IdleState()
    {
        if (ap > 100)
        {
            SetState(State.Attack);
            animator.SetTrigger("Charge");
        }
        else
            ap += speed * Time.deltaTime;
        animator.SetFloat("Speed", ap);
    }

    protected override void MoveState(){ }
}
