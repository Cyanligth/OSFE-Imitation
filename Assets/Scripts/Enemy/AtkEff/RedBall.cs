using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : AttackBase
{
    [SerializeField] float speed;
    private void OnEnable()
    {
        atkData.IsEnded.AddListener((v2) => { Check(v2); });
    }
    private void OnDisable()
    {
        atkData.IsEnded.RemoveListener(Check);
    }
    private void Start()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (transform.position.x > GameManager.Data.map[0, 0].x - 1)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
            yield return null;
        }
        GameManager.Resource.Destroy(gameObject);
    }
    public void Check(Vector2 v2)
    {
        if (Vector2.Distance(v2 + new Vector2(0, 1), transform.position) < 1f)
            GameManager.Resource.Destroy(gameObject);
    }
}
