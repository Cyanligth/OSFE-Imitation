using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : AttackBase
{
    [SerializeField] float t;
    [SerializeField] float speed;
    Vector3 curPos;
    Coroutine coroutine;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        coroutine = StartCoroutine(Move());
        atkData.IsEnded.AddListener((vector2) => { Check(vector2); });
        StartCoroutine(atkData.ProjectileAttack(GameManager.Data.playerXPos, GameManager.Data.playerYPos, 2, 0.08f, playerAtkMask, false));
        
    }
    IEnumerator Move()
    {
        curPos = transform.position;
        float time = 0;
        while (time < t) 
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
    private void Check(Vector2 vector2)
    {
        if (Vector2.Distance(vector2 + new Vector2(0,1), transform.position) < 1f)
            GameManager.Resource.Destroy(gameObject);
    }
}
