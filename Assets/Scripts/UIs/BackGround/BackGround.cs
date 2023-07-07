using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void Active()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Scrolling(float time)
    {
        StartCoroutine(ScrollRoutine(time));
    }
    IEnumerator ScrollRoutine(float time)
    {
        float t = 0;
        while (t < time)
        {
            transforms[$"{gameObject.name}-Bg"].anchoredPosition += new Vector2(-3840, 0) * Time.deltaTime;
            transforms[$"{gameObject.name}-Scrolling"].anchoredPosition += new Vector2(-3840, 0) * Time.deltaTime;
            if (transforms[$"{gameObject.name}-Bg"].anchoredPosition.x <= -1920)
                transforms[$"{gameObject.name}-Bg"].anchoredPosition = new Vector2(1920, 0);
            if(transforms[$"{gameObject.name}-Scrolling"].anchoredPosition.x <= -1920)
                transforms[$"{gameObject.name}-Scrolling"].anchoredPosition = new Vector2(1920, 0);
            t += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        transforms[$"{gameObject.name}-Bg"].anchoredPosition = new Vector2(0, 0);
        transforms[$"{gameObject.name}-Scrolling"].anchoredPosition = new Vector2(1920, 0);
    }
}
