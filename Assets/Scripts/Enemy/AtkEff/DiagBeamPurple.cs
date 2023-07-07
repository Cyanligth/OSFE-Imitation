using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagBeamPurple : AttackBase
{
    public int x;
    public int y;
    public void Beam()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        float time = 0;
        float check = 0;
        if (y == 2)
            check = 2;
        atkData.TargetAttack(x, y, 40, enemyAtkMask, out bool b);
        while (time < 4)
        {
            switch (check)
            {
                case 0:
                    x -= 1; y += 1;
                    break;
                case 1:
                    x += 1; y += 1;
                    break; 
                case 2:
                    x += 1; y -= 1;
                    break; 
                case 3:
                    x -= 1; y -= 1;
                    break;
            }
            StartCoroutine(MoveCorutine(GameManager.Data.map[x, y]));
            atkData.TargetAttack(x, y, 40, enemyAtkMask, out b);
            check = (check + 1)%4 ;
            time += 0.5f;
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
