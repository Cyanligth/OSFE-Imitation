using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLine : MonoBehaviour
{
    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void Move(Vector2 endPos)
    {
        StartCoroutine(MoveRoutine(endPos));
    }
    IEnumerator MoveRoutine(Vector2 endPos)
    {
        float rate = 0;
        Vector2 startPos = rect.position;
        while(rate < 1)
        {
            rect.position = new Vector2(rect.position.x, Mathf.Lerp(startPos.y, endPos.y, rate));
            rate += Time.deltaTime * 20;
            yield return null;
        }    
    }
}
