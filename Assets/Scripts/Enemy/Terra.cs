using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Terra : BaseEnemy
{
    Animator animator;
    int actionCount = 0;
    int atkPattern = 0;
    bool atkEnd;
    protected override void Awake()
    {
        enemyData = GameManager.Resource.Load<EnemyData>("Data/Enemy/Terra");
        base.Awake();
        animator = gameObject.GetComponent<Animator>();
    }
    protected override void Start()
    {
        base.Start();
        atkEnd = true;
        SetStartPosition(7, 0);
        
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void AttackState()
    {
        switch(atkPattern)
        {
            case 0:
                animator.SetInteger("Attack", 1);
                StartCoroutine(AtkPattern1());
                break;
            case 1:
                animator.SetInteger("Attack", 2);
                StartCoroutine(AtkPattern2());
                break; 
            case 2:
                animator.SetInteger("Attack", 3);
                StartCoroutine(AtkPattern3());
                break; 
        }
        SetState(State.Idle);
    }
    IEnumerator AtkPattern1()
    {
        int x = GameManager.Data.playerXPos;
        int y = GameManager.Data.playerYPos;
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if(i != x && j != y)
                {
                    GameObject warning = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[i, j], Quaternion.identity);
                    GameManager.Resource.Destroy(warning, 2f);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("Attack", 0);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i != x && j != y)
                {
                    WarningTimer timer = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[i, j], Quaternion.identity);
                    timer.StartTimer(1f);
                    int a = i;
                    int b = j;
                    timer.TimeOver.AddListener(() => { BreakerBeamGenerate(a, b); });
                }
            }
        }
        int t = 0;
        yield return new WaitForSeconds(1);
        while (t < 4)
        {
            int p = Random.Range(0, 4);
            int i = 0;
            int j = 0;
            int k = 4;
            int l = 4;
            switch (p) 
            { 
                case 0:
                    i += 1;
                    break;
                case 1:
                    j += 1;
                    break;
                case 2:
                    k -= 1;
                    break;
                case 3:
                    l -= 1;
                    break;
            }
            for (int g = i; g < k; g++)
            {
                for (int h = j; h < l; h++)
                {
                    GameObject danger = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningDanger", GameManager.Data.map[g, h], Quaternion.identity);
                    GameManager.Resource.Destroy(danger, 2f);
                }
            }
            yield return new WaitForSeconds(0.5f);
            for (int g = i; g < k; g++)
            {
                for (int h = j; h < l; h++)
                {
                    WarningTimer timer = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[g, h], Quaternion.identity);
                    timer.StartTimer(1.5f);
                    int a = g;
                    int b = h;
                    timer.TimeOver.AddListener(() => { BeamGen(a, b); });
                }
            }
            t++;
            yield return new WaitForSeconds(1);
        }
        atkEnd = true;
    }
    IEnumerator AtkPattern2()
    {
        
        GameObject danger1 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningDanger", GameManager.Data.map[3, 0], Quaternion.identity);
        GameObject danger2 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningDanger", GameManager.Data.map[0, 3], Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        WarningTimer dangerTimer1 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[3, 0], Quaternion.identity);
        WarningTimer dangerTimer2 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[0, 3], Quaternion.identity);
        dangerTimer1.StartTimer(1); dangerTimer1.TimeOver.AddListener(() => { BreakDiscGen(3, 0); GameManager.Resource.Destroy(danger1); });
        dangerTimer2.StartTimer(1); dangerTimer2.TimeOver.AddListener(() => { BreakDiscGen(0, 3); GameManager.Resource.Destroy(danger2); });
        if (curHp > 1500)
        {
            int j = Random.Range(1, 3);
            GameObject warning1 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[0, j], Quaternion.identity);
            GameObject warning2 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[1, j], Quaternion.identity);
            GameObject warning3 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[2, j], Quaternion.identity);
            GameObject warning4 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[3, j], Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            animator.SetInteger("Attack", 0);
            WarningTimer timer1 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[0, j], Quaternion.identity);
            WarningTimer timer2 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[1, j], Quaternion.identity);
            WarningTimer timer3 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[2, j], Quaternion.identity);
            WarningTimer timer4 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[3, j], Quaternion.identity);
            timer1.StartTimer(1f); timer1.TimeOver.AddListener(() => { BreakerBeamGenerate(0, j); GameManager.Resource.Destroy(warning1); });
            timer2.StartTimer(1f); timer2.TimeOver.AddListener(() => { BreakerBeamGenerate(1, j); GameManager.Resource.Destroy(warning2); });
            timer3.StartTimer(1f); timer3.TimeOver.AddListener(() => { BreakerBeamGenerate(2, j); GameManager.Resource.Destroy(warning3); });
            timer4.StartTimer(1f); timer4.TimeOver.AddListener(() => { BreakerBeamGenerate(3, j); GameManager.Resource.Destroy(warning4); });

        }
        else
        {
            GameObject warning1 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[2, 0], Quaternion.identity);
            GameObject warning2 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[0, 1], Quaternion.identity);
            GameObject warning3 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[1, 1], Quaternion.identity);
            GameObject warning4 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[2, 1], Quaternion.identity);
            GameObject warning5 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[3, 1], Quaternion.identity);
            GameObject warning6 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[2, 2], Quaternion.identity);
            GameObject warning7 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[2, 3], Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            animator.SetInteger("Attack", 0);
            WarningTimer timer1 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[2, 0], Quaternion.identity);
            WarningTimer timer2 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[0, 1], Quaternion.identity);
            WarningTimer timer3 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[1, 1], Quaternion.identity);
            WarningTimer timer4 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[2, 1], Quaternion.identity);
            WarningTimer timer5 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[3, 1], Quaternion.identity);
            WarningTimer timer6 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[2, 2], Quaternion.identity);
            WarningTimer timer7 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[2, 3], Quaternion.identity);
            timer1.StartTimer(2f); timer1.TimeOver.AddListener(() => { BreakerBeamGenerate(2, 0); GameManager.Resource.Destroy(warning1); });
            timer2.StartTimer(2f); timer2.TimeOver.AddListener(() => { BreakerBeamGenerate(0, 1); GameManager.Resource.Destroy(warning2); });
            timer3.StartTimer(2f); timer3.TimeOver.AddListener(() => { BreakerBeamGenerate(1, 1); GameManager.Resource.Destroy(warning3); });
            timer4.StartTimer(2f); timer4.TimeOver.AddListener(() => { BreakerBeamGenerate(2, 1); GameManager.Resource.Destroy(warning4); });
            timer5.StartTimer(2f); timer5.TimeOver.AddListener(() => { BreakerBeamGenerate(3, 1); GameManager.Resource.Destroy(warning5); });
            timer6.StartTimer(2f); timer6.TimeOver.AddListener(() => { BreakerBeamGenerate(2, 2); GameManager.Resource.Destroy(warning6); });
            timer7.StartTimer(2f); timer7.TimeOver.AddListener(() => { BreakerBeamGenerate(2, 3); GameManager.Resource.Destroy(warning7); });
        }
        atkEnd = true;
    }
    IEnumerator AtkPattern3()
    {
        GameObject warning1 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[0, 0], Quaternion.identity);
        GameObject warning2 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[3, 0], Quaternion.identity);
        GameObject warning3 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[0, 3], Quaternion.identity);
        GameObject warning4 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningBreak", GameManager.Data.map[3, 3], Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        WarningTimer timer1 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[0, 0], Quaternion.identity);
        WarningTimer timer2 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[3, 0], Quaternion.identity);
        WarningTimer timer3 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[0, 3], Quaternion.identity);
        WarningTimer timer4 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[3, 3], Quaternion.identity);
        timer1.StartTimer(2.5f); timer1.TimeOver.AddListener(() => { BreakerBeamGenerate(0, 0); GameManager.Resource.Destroy(warning1); });
        timer2.StartTimer(2.5f); timer2.TimeOver.AddListener(() => { BreakerBeamGenerate(3, 0); GameManager.Resource.Destroy(warning2); });
        timer3.StartTimer(2.5f); timer3.TimeOver.AddListener(() => { BreakerBeamGenerate(0, 3); GameManager.Resource.Destroy(warning3); });
        timer4.StartTimer(2.5f); timer4.TimeOver.AddListener(() => { BreakerBeamGenerate(3, 3); GameManager.Resource.Destroy(warning4); });
        yield return new WaitForSeconds(0.2f);
        int i = Random.Range(1, 3); int j = Random.Range(1, 3);
        GameObject danger1 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningDanger", GameManager.Data.map[i,j], Quaternion.identity);
        GameObject danger2 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningDanger", GameManager.Data.map[i,j], Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        WarningTimer dangerTimer1 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[i, j], Quaternion.identity);
        dangerTimer1.StartTimer(0.7f); dangerTimer1.TimeOver.AddListener(() => { DiagBeamGen(i, j); GameManager.Resource.Destroy(danger1); });
        yield return new WaitForSeconds(0.2f);
        WarningTimer dangerTimer2 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[i, j], Quaternion.identity);
        dangerTimer2.StartTimer(0.7f); dangerTimer2.TimeOver.AddListener(() => { DiagBeamGen(i, j); GameManager.Resource.Destroy(danger2); });
        yield return new WaitForSeconds(1f);
        if(curHp < 1500)
        {
            int g = Random.Range(1, 3); int h = Random.Range(1, 3);
            GameObject danger3 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningDanger", GameManager.Data.map[g, h], Quaternion.identity);
            GameObject danger4 = GameManager.Resource.Instantiate<GameObject>("Prefabs/WarningDanger", GameManager.Data.map[g, h], Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            WarningTimer dangerTimer3 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[g, h], Quaternion.identity);
            dangerTimer3.StartTimer(0.7f); dangerTimer3.TimeOver.AddListener(() => { DiagBeamGen(g, h); GameManager.Resource.Destroy(danger3); });
            yield return new WaitForSeconds(0.2f);
            WarningTimer dangerTimer4 = GameManager.Resource.Instantiate<WarningTimer>("Prefabs/WarningTimer", GameManager.Data.map[g, h], Quaternion.identity);
            dangerTimer4.StartTimer(0.7f); dangerTimer4.TimeOver.AddListener(() => { DiagBeamGen(g, h); GameManager.Resource.Destroy(danger4); });
        }
        yield return new WaitForSeconds(3);
        animator.SetInteger("Attack", 0);
        atkEnd = true;
    }
    private void BreakerBeamGenerate(int x, int y)
    {
        GameManager.Resource.Instantiate<BreakerBeam>("Effect/Enemy/BreakerBeam", GameManager.Data.map[x, y], Quaternion.identity);
        GameManager.Data.BreakTile(x, y, 7);
        attackRoutineData.TargetAttack(x, y, atk, mask, out bool b);
    }
    private void DiagBeamGen(int x, int y)
    {
        DiagBeamCharge beamCharge = GameManager.Resource.Instantiate<DiagBeamCharge>("Effect/Enemy/DiagBeamCharge", GameManager.Data.map[x, y], Quaternion.identity);
        beamCharge.x = x;
        beamCharge.y = y;
    }
    private void BreakDiscGen(int x, int y)
    {
        BreakDisc disc = GameManager.Resource.Instantiate<BreakDisc>("Effect/Enemy/BreakerDisc", GameManager.Data.map[x,y], Quaternion.identity);
        disc.x = x;
        disc.y = y;
        disc.Disc();
    }
    private void BeamGen(int x, int y)
    {
        GameManager.Resource.Instantiate<BreakerBeam>("Effect/Enemy/BreakerBeam", GameManager.Data.map[x, y], Quaternion.identity);
        attackRoutineData.TargetAttack(x, y, atk, mask, out bool b);
    }
    protected override void DeadState()
    {
        animator.SetBool("Down", true);
        curHp = 1;
        OnHit?.Invoke();
        SetState(State.Idle);
    }

    protected override void FindState()
    {
        
    }

    protected override void HitState()
    {
        animator.SetTrigger("Hit");
        if(animator.GetBool("Down"))
        {
            GameManager.Resource.Destroy(gameObject);
            return;
        }
        if (curHp <= 0)
        {
            SetState(State.Dead);
            return;
        }
        SetState(State.Idle);
    }

    protected override void IdleState()
    {
        if (animator.GetBool("Down")) return;
        ap += speed * Time.deltaTime;
        if(ap >= 701 && atkEnd)
        {
            ap = 0;
            if(actionCount != 0)
            {
                SetState(State.Move);
                actionCount = 0;
                return;
            }
            actionCount += 1;
            atkEnd = false;
            atkPattern = Random.Range(0, 3);
            SetState(State.Attack);
        }
    }

    protected override void MoveState()
    {
        StartCoroutine(RandomBlink());
        SetState(State.Idle);
    }
    IEnumerator RandomBlink()
    {
        int i = 0;
        while (i < 8)
        {
            animator.SetTrigger("Blink");
            SetPosition(Random.Range(4, 8), Random.Range(0, 4));
            i++;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
