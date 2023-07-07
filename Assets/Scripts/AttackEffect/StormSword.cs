using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSword : AttackBase
{
    private int count;
    private int maxCount;
    private void Start()
    {
        count = GameManager.Data.playerXPos + 1;
        maxCount = 8;
        StartCoroutine(StormSwordClone());
    }
    IEnumerator StormSwordClone()
    {
        int y = GameManager.Data.playerYPos;
        while (count < maxCount)
        {
            yield return new WaitForSeconds(0.1f);
            GameManager.Resource.Instantiate<GameObject>("Effect/Card/StormSwordClone", GameManager.Data.map[count, y], Quaternion.identity);
            Collider2D collider = atkData.TargetAttack(count, y, 10, playerAtkMask, out bool b);
            BaseEnemy enemy = collider?.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.ap -= 100;
            }
            count++;
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
}
