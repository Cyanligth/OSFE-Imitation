using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : BaseNpc, IStageEventListener
{
    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Hp = 100;
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Communicate()
    {
        GameManager.Player.Heal(200);
    }

    public void GameStartEvent()
    {
    }

    public void NextStageEvent(string nextStage)
    {

    }

    public void NextWorldEvent()
    {
    }

    public void ReadyToBattleEvent()
    {
        // �����ּ��� ���
    }

    public void StageClearEvent()
    {
        // ������ ����ϰ� �÷��̾� ��ġ�� ������ ����
        animator.SetTrigger("Heal");
        Communicate();
    }

    protected override void IsHited()
    {
        if(Hp <= 0)
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
