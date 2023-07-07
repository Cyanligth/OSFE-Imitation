using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class WarningTimer : MonoBehaviour
{
    public UnityEvent TimeOver;
    SpriteRenderer spriteRenderer;
    float time;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void StartTimer(float t)
    {
        StartCoroutine(WarningTimerRoutine(t));
    }
    public void SetTime(float t)
    {
        time = t;
    }
    IEnumerator WarningTimerRoutine(float time)
    {
        float rate = 0;
        float t = 0;
        while (t < time) 
        {
            float scale = Mathf.Lerp(1.8f, 1, rate);
            float color = Mathf.Lerp(0, 255, rate);
            transform.localScale = new Vector3(scale, scale);
            spriteRenderer.color = new Color(color, 0, 0);
            t += Time.deltaTime;
            rate += ((1 / time) * Time.deltaTime);
            yield return null;
        }
        TimeOver?.Invoke();
        TimeOver.RemoveAllListeners();
        GameManager.Resource.Destroy(gameObject);
    }
}
