using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticWave : AttackBase
{
    private int count = 0;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        transform.position = GameManager.Data.map[GameManager.Data.playerXPos+1, GameManager.Data.playerYPos];
        StartCoroutine(KineticWaveClone());
    }
    IEnumerator KineticWaveClone()
    {
        int x = GameManager.Data.playerXPos;
        int y = GameManager.Data.playerYPos;
        Collider2D collider = atkData.TargetAttack(++x, y, 40, playerAtkMask, out bool b);
        if (collider != null)
        {
            BaseEnemy baseEnemy = collider?.GetComponent<BaseEnemy>();
            if (baseEnemy != null && baseEnemy.xPos < 7)
                baseEnemy.SetPosition(baseEnemy.xPos + 1, baseEnemy.yPos);
        }
        while (count < 3)
        {
            count++;
            yield return new WaitForSeconds(0.25f);
            collider = atkData.TargetAttack(++x, y, 40, playerAtkMask, out b);
            if (collider != null)
            {
                BaseEnemy baseEnemy = collider?.GetComponent<BaseEnemy>();
                if (baseEnemy != null && baseEnemy.xPos < 7)
                    baseEnemy.SetPosition(baseEnemy.xPos + 1, baseEnemy.yPos);
            }
            GameManager.Resource.Instantiate<GameObject>("Effect/Card/KineticWaveClone", new Vector3(transform.position.x + (1.6f * count), transform.position.y), transform.rotation);
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
}
