using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : BaseUI
{
    private Slider slider;
    RectTransform rect;
    Coroutine manaRegenerating;
    protected override void Awake()
    {
        base.Awake();
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        if(manaRegenerating != null)
            StopCoroutine(manaRegenerating);

        slider.maxValue = GameManager.Player.MaxMana;
        slider.value = GameManager.Player.CurMana;

        float w = rect.rect.width / GameManager.Player.MaxMana;

        for (int i = 1; i < GameManager.Player.MaxMana; i++)
        {
            RectTransform rectt = GameManager.Resource.Instantiate<RectTransform>("UI/ManaLine", Vector3.zero, Quaternion.identity, transform);
            rectt.anchoredPosition = new Vector2(w * i, 2.75f);
        }
        manaRegenerating = StartCoroutine(ManaRegenerating());
    }
    IEnumerator ManaRegenerating()
    {
        while (true)
        {
            if (GameManager.Player.CurMana < GameManager.Player.MaxMana)
            {
                GameManager.Player.CurMana += GameManager.Player.ManaRegen * Time.deltaTime;
            }
            if (GameManager.Player.CurMana < 0)
                GameManager.Player.CurMana = 0;
            slider.value = GameManager.Player.CurMana;
            texts["ManaText"].text = ((int)GameManager.Player.CurMana).ToString() + "/" + GameManager.Player.MaxMana.ToString();
            yield return new WaitForFixedUpdate();
        }
    }
}
