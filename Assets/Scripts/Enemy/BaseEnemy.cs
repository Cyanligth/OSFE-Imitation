using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHittable
{
    public enum State { Idle, Find, Move, Attack, Hit, Dead }
    protected State curState;
    public EnemyData enemyData;
    public string enemyName;
    public int curHp;
    public int speed;
    public float ap;
    public int atk;
    
    public Vector2 pos;
    public int xPos;
    public int yPos;

    protected AttackRoutineData attackRoutineData;

    protected virtual void Awake()
    {
        attackRoutineData = GameManager.Resource.Load<AttackRoutineData>("Data/AttackRoutineData");
        curState = State.Idle;
    }
    protected virtual void Start()
    {
        curHp = enemyData.hp;
        speed = enemyData.speed;
        atk = enemyData.atk;
    }
    
    public void SetState(State state)
    {
        curState = state;
    }
    public void SetPosition(int xPos, int yPos)
    {
        this.xPos = xPos; this.yPos = yPos;
        transform.position = new Vector3(GameManager.Data.map[xPos, yPos].x, GameManager.Data.map[xPos, yPos].y);
    }

    protected virtual void Update()
    {
        switch ((int)curState)
        {
            case 0: IdleState(); break;
            case 1: FindState(); break;
            case 2: MoveState(); break;
            case 3: AttackState(); break;
            case 4: HitState(); break;
            case 5: DeadState(); break;
            default: break;
        }
    }
    protected abstract void IdleState();
    protected abstract void FindState();
    protected abstract void MoveState();
    protected abstract void AttackState();
    protected abstract void HitState();
    protected abstract void DeadState();

    protected IEnumerator MoveCorutine(Vector2 startPos, Vector2 endPos)
    {
        float totalTime = Vector2.Distance(startPos, endPos) / 20;
        float rate = 0;
        while (rate < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, rate);
            rate += Time.deltaTime / totalTime;
            yield return null;
        }
    }

    public void Hit(int damage)
    {
        curHp -= damage;
        SetState(State.Hit);
    }
}
