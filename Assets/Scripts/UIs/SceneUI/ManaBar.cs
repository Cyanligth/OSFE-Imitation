using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : BaseUI
{
    private Slider slider;
    Coroutine manaRegenerating;
    protected override void Awake()
    {
        base.Awake();
        slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        slider.maxValue = GameManager.Player.MaxMana;
        slider.value = GameManager.Player.CurMana;
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
