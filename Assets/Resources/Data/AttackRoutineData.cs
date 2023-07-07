using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AttackRoutineData", menuName = "Data/AttackRoutine")]
public class AttackRoutineData : ScriptableObject
{
    public Vector2 TargetOverlapBox = new Vector2(1.5f, 0.85f);
    public UnityEvent<Vector2> IsEnded;

    public IEnumerator ProjectileAttack(int xPos, int yPos, int damage, float time, LayerMask mask, bool pierce)
    {
        bool b;
        if (xPos < 4)
        {
            while (xPos < 7)
            {
                TargetAttack(++xPos, yPos, damage, mask, out b);
                if (!pierce && b)
                {
                    IsEnded?.Invoke(GameManager.Data.map[xPos, yPos]);
                    yield break;
                }
                yield return new WaitForSeconds(time);
            }
        }
        else
        {
            while(xPos > 0)
            {
                TargetAttack(--xPos, yPos, damage, mask, out b);
                if (!pierce && b)
                {
                    IsEnded?.Invoke(GameManager.Data.map[xPos, yPos]);
                    yield break;
                }
                yield return new WaitForSeconds(time);
            }
        }
        yield return null;
    }
    public IEnumerator ProjectileAttack(int xPos, int yPos, int height, int damage, float time, LayerMask mask)
    {
        Collider2D[] colliders;
        float h = (height - 1) * 0.5f;
        if (xPos < 4)
        {
            while (xPos < 7)
            {
                xPos++;
                int y = yPos + (int)h;
                for (int i = 0; i < h * 2 + 1; i++)
                {
                    if (y - i > -1 && y - i < 4)
                        GameManager.Data.tileList[xPos, y - i].SetTileRoutine(1, 0.2f);
                }
                colliders = Physics2D.OverlapAreaAll(GameManager.Data.map[xPos, yPos] + new Vector2(-0.75f, 0.425f + (0.85f * h)),
                    GameManager.Data.map[xPos, yPos] + new Vector2(0.75f, -(0.425f + (0.85f * h))), mask);
                foreach (Collider2D collider in colliders)
                {
                    IHittable hittable = collider?.GetComponent<IHittable>();
                    if (hittable != null)
                    {
                        hittable.Hit(damage);
                    }
                }
                yield return new WaitForSeconds(time);
            }
        }
        else
        {
            while (xPos > 0)
            {
                xPos--;
                int y = yPos + (int)h;
                for (int i = 0; i < h * 2 + 1; i++)
                {
                    if(y - i > -1 && y - i < 4)
                        GameManager.Data.tileList[xPos, y - i].SetTileRoutine(1, 0.2f); 
                }
                colliders = Physics2D.OverlapAreaAll(GameManager.Data.map[xPos, yPos] + new Vector2(-0.75f, 0.425f + (0.85f * h)),
                    GameManager.Data.map[xPos, yPos] + new Vector2(0.75f, -(0.425f + (0.85f * h))), mask);
                foreach (Collider2D collider in colliders)
                {
                    IHittable hittable = collider?.GetComponent<IHittable>();
                    if (hittable != null)
                    {
                        hittable.Hit(damage);
                    }
                }
                yield return new WaitForSeconds(time);
            }
        }
        yield return null;
    }
    public void BeamAttack(int xPos, int yPos, int damage, LayerMask mask)
    {
        Collider2D[] colliders;
        if(xPos < 4)
        {
            colliders = Physics2D.OverlapAreaAll(GameManager.Data.map[xPos+1, yPos] + new Vector2(-0.75f, 0.425f),
            GameManager.Data.map[7, yPos] + new Vector2(0.75f, -0.425f), mask);
            for (int i = xPos+1; i < 8; i++)
            {
                GameManager.Data.tileList[i, yPos].SetTileRoutine(1, 0.5f);
            }
        }
        else
        {
            colliders = Physics2D.OverlapAreaAll(GameManager.Data.map[xPos-1, yPos] + new Vector2(0.75f, -0.425f),
            GameManager.Data.map[0, yPos] + new Vector2(-0.75f, 0.425f), mask);
            for (int i = xPos-1; i > -1; i--)
            {
                GameManager.Data.tileList[i, yPos].SetTileRoutine(1, 0.5f);
            }
        }
        
        foreach (Collider2D collider in colliders)
        {
            IHittable hittable = collider?.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.Hit(damage);
            }
        }
    }
    public void BeamAttack(int xPos, int yPos, int height, int damage, LayerMask mask)
    {
        float h = (height - 1) * 0.5f;
        Collider2D[] colliders;
        if(xPos < 4)
        {
            colliders = Physics2D.OverlapAreaAll(GameManager.Data.map[xPos, yPos] + new Vector2(-0.75f, 0.425f + (0.85f * h)),
            GameManager.Data.map[7, yPos] + new Vector2(0.75f, -(0.425f + (0.85f * h))), mask);
            for (int i = xPos; i < 8; i++)
            {
                for (int j = yPos - (int)h; j < yPos + h; j++)
                {
                    GameManager.Data.tileList[xPos, yPos].SetTileRoutine(1, 0.5f);
                }
            }
        }
        else
        {
            colliders = Physics2D.OverlapAreaAll(GameManager.Data.map[xPos, yPos] + new Vector2(0.75f, -(0.425f + (0.85f * h))),
            GameManager.Data.map[0, yPos] + new Vector2(-0.75f, 0.425f + (0.85f * h)), mask);
            for (int i = xPos; i > -1; i--)
            {
                for (int j = yPos - (int)h; j < yPos + h; j++)
                {
                    GameManager.Data.tileList[xPos, yPos].SetTileRoutine(1, 0.5f);
                }
            }
        }

        foreach (Collider2D collider in colliders)
        {
            IHittable hittable = collider?.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.Hit(damage);
            }
        }
    }
    public Collider2D TargetAttack(int xPos, int yPos, int damage, LayerMask mask, out bool b )
    {
        if(xPos > 7 || xPos < 0 || yPos < 0 || yPos > 3)
        {
            b = false;
            return null;
        }
        Collider2D[] colliders = Physics2D.OverlapBoxAll(GameManager.Data.map[xPos,yPos], TargetOverlapBox, 0, mask);
        GameManager.Data.tileList[xPos, yPos].SetTileRoutine(1, 0.2f);
        if (colliders.Length < 1)
        {
            b = false;
            return null;
        }
        foreach (Collider2D collider in colliders)
        {
            IHittable hittable = collider?.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.Hit(damage);
                b = true;
                return collider;
            }
        }
        b = false;
        return null;
    }
    public IEnumerator FieldAttack(int sXpos, int sYpos, int eXpos, int eYpos, int damage, LayerMask mask)
    {
        yield return null;
    }
}
