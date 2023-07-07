using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour, IHittable
{
    public UnityEvent OnHit;
    private int hp;
    public int xPos;
    public int yPos;
    public int Hp { get { return hp; } }
    private void OnEnable()
    {
        hp = 80;
        NpcObjFollowingUI canvas = GameManager.Resource.Instantiate<NpcObjFollowingUI>("UI/NpcObjFollowingCanvas", transform);
        canvas.transform.position = transform.position;
    }
    public void SetPosition(int x, int y)
    {
        if(!GameManager.Data.boolMap[xPos, yPos])
            GameManager.Data.boolMap[xPos, yPos] = true;
        transform.position = GameManager.Data.map[x, y];
        GameManager.Data.boolMap[x, y] = false;
        this.yPos = y;
        this.xPos = x;
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
        if(hp <= 0)
        {
            GameManager.Data.boolMap[xPos, yPos] = true;
            GameManager.Resource.Destroy(gameObject);
        }
            
    }
}
