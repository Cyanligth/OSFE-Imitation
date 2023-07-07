using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FrostBolt : AttackBase
{
    private void Start()
    {
        transform.position = GameManager.Player.PlayerPos + new Vector2(0.5f, 1);
        atkData.IsEnded.AddListener((v2) => { Check(v2); });
        StartCoroutine(atkData.ProjectileAttack(GameManager.Data.playerXPos, GameManager.Data.playerYPos, 50, 0.08f, playerAtkMask, false));
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        float time = 0;
        while(time < 1.5f)
        {
            transform.position += Vector3.right * 20 * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
    private void Check(Vector2 vector2)
    {
        if (Vector2.Distance(vector2 + new Vector2(0, 1f), transform.position) < 2f)
            GameManager.Resource.Destroy(gameObject);
    }
}
