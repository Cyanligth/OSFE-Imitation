using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour, IHittable
{
    public enum State { Idle, Find, Move, Attack, Hit, Dead }
    protected State curState;
    protected LayerMask mask;
    public EnemyData enemyData;
    public string enemyName;
    public int curHp;
    public int speed;
    public float ap; // Çàµ¿·Â
    public int atk;
    public UnityEvent OnHit;
    
    public Vector2 pos;
    public int xPos;
    public int yPos;

    protected AttackRoutineData attackRoutineData;

    protected virtual void Awake()
    {
        attackRoutineData = GameManager.Resource.Load<AttackRoutineData>("Data/AttackRoutineData");
        curState = State.Idle;
        mask = LayerMask.GetMask("Player", "Npc", "Envi");
    }
    protected virtual void Start()
    {
        curHp = enemyData.hp;
        speed = enemyData.speed + GameManager.Player.Luck;
        atk = enemyData.atk;
        EnemyFollowingCanvas canvas = GameManager.Resource.Instantiate<EnemyFollowingCanvas>("UI/EnemyFollowingCanvas", transform);
        canvas.transform.position = transform.position;
    }
    
    public void SetState(State state)
    {
        curState = state;
    }
    public bool SetStartPosition(int xPos, int yPos)
    {
        if (GameManager.Data.isTileUsed[xPos, yPos])
            return false;
        this.xPos = xPos; this.yPos = yPos;
        transform.position = GameManager.Data.map[xPos, yPos];
        GameManager.Data.isTileUsed[xPos, yPos] = true;
        return true;
    }
    public bool SetPosition(int xPos, int yPos)
    {
        if(xPos > 7)
        {
            xPos = 7;
            this.xPos = 7;
        }
        if(xPos < 0)
        {
            xPos = 0;
            this.xPos = 0;
        }
        if (yPos > 3)
        {
            yPos = 3;
            this.yPos = 3;
        }
        if (yPos < 0)
        {
            yPos = 0;
            this.yPos = 0;
        }
        if (GameManager.Data.isTileUsed[xPos, yPos] || GameManager.Data.boolMap[xPos, yPos])
            return false;
        GameManager.Data.isTileUsed[this.xPos, this.yPos] = false;
        this.xPos = xPos; this.yPos = yPos;
        StartCoroutine(MoveCorutine(transform.position, GameManager.Data.map[xPos, yPos]));
        GameManager.Data.isTileUsed[this.xPos, this.yPos] = true;
        return true;
    }
    public void SetTemporaryPos(int xPos, int yPos)
    {
        this.xPos = xPos; this.yPos = yPos;
        StartCoroutine(MoveCorutine(transform.position, GameManager.Data.map[xPos, yPos]));
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
    protected IEnumerator MoveCorutine(Vector2 startPos, Vector2 endPos, float speed)
    {
        float totalTime = Vector2.Distance(startPos, endPos) / speed;
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
        if (damage < 50)
            GameManager.Sound.Play("enemy_hit_light");
        else
        {
            GameManager.Sound.Play("enemy_hit_heavy");
        }
        curHp -= damage;
        OnHit?.Invoke();
        SetState(State.Hit);
    }
    protected void OnDestroy()
    {
        GameManager.Data.IsStageClear();
    }
}
