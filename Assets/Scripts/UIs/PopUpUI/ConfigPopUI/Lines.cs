using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void MoveLine(Vector2 endPos)
    {
        StartCoroutine(Move(endPos));
    }
    IEnumerator Move(Vector2 endPos)
    {
        Vector2 startPos = rect.position;
        float rate = 0;
        while (rate < 1)
        {
            rect.position = new Vector3(Mathf.Lerp(startPos.x, endPos.x, rate), Mathf.Lerp(startPos.y, endPos.y, rate));
            rate += 0.01f * 20;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
