using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDisc : AttackBase
{
    public int x;
    public int y;
    int dir = 0;
    public void Disc()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        float t = 0;
        atkData.TargetAttack(x,y, 50, enemyAtkMask, out bool b);
        while(t < 6)
        {
            int count = 0;
            a:
            switch (dir)
            {
                case 0:
                    if (count++ > 4) break;
                    if (x - 1 < 0) { dir = 1; goto a; }
                    else if (!GameManager.Data.boolMap[x - 1, y]) { dir = 1; goto a; }
                    else x -= 1;
                    break;
                case 1:
                    if (count++ > 4) break;
                    if (y + 1 > 3) { dir = 2; goto a; }
                    else if (!GameManager.Data.boolMap[x, y + 1]) { dir = 2; goto a; }
                    else y += 1;
                    break;
                case 2:
                    if (count++ > 4) break;
                    if (x + 1 > 7) { dir = 3; goto a; }
                    else if (!GameManager.Data.boolMap[x + 1, y]) { dir = 3; goto a; }
                    else x += 1;
                    break;
                case 3:
                    if (count++ > 4) break;
                    if (y - 1 < 0) { dir = 0; goto a; }
                    else if (!GameManager.Data.boolMap[x, y - 1]) { dir = 0; goto a; }
                    else y -= 1;
                    break;
            }
            StartCoroutine(MoveCorutine(GameManager.Data.map[x,y]));
            atkData.TargetAttack(x, y, 50, enemyAtkMask, out b);
            t += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.Resource.Destroy(gameObject);
    }
    protected IEnumerator MoveCorutine(Vector2 endPos)
    {
        Vector2 startPos = transform.position;
        float rate = 0;
        while (rate < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, rate);
            rate += Time.deltaTime * 20;
            yield return null;
        }
    }
}
