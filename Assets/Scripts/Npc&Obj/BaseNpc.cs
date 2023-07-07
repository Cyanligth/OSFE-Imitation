using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseNpc : MonoBehaviour, IHittable
{
    private int hp;
    public string npcName;
    public int xPos;
    public int yPos;
    public SpriteRenderer spriteRenderer;
    public UnityEvent OnHit;
    protected EventMaster master;
    public int Hp { get { return hp; } set { hp = value; } }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
    }
    public void SetPosition(int xPos, int yPos, float speed)
    {
        this.xPos = xPos; this.yPos = yPos;
        StartCoroutine(MoveCorutine(transform.position, GameManager.Data.map[xPos, yPos], speed));
    }

    protected virtual void Start()
    {
        NpcObjFollowingUI canvas = GameManager.Resource.Instantiate<NpcObjFollowingUI>("UI/NpcObjFollowingCanvas", transform);
        canvas.transform.position = transform.position;
    }
    public abstract void Communicate();

    protected bool isMove;
    protected IEnumerator MoveCorutine(Vector2 startPos, Vector2 endPos, float speed)
    {
        float totalTime = Vector2.Distance(startPos, endPos) / speed;
        float rate = 0;
        isMove = true;
        while (rate < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, rate);
            rate += Time.deltaTime / totalTime;
            yield return null;
        }
        isMove = false;
    }
    public void Hit(int damage)
    {
        if (damage < 50)
            GameManager.Sound.Play("enemy_hit_light");
        else
        {
            GameManager.Sound.Play("enemy_hit_heavy");
        }
        OnHit?.Invoke();
        hp -= damage;
        IsHited();
    }
    protected abstract void IsHited();
}
