using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData;
    protected Dictionary<string, TMP_Text> texts;
    protected Dictionary<string, Image> images;
    protected virtual void Awake()
    {
        BindChildren();
    }

    protected virtual void OnEnable()
    {
        texts["CardNameText"].text = cardData.name.ToUpper();
        texts["UseManaText"].text = cardData.useMana.ToString();
        texts["DamageText"].text = cardData.damage.ToString();
        texts["EffectText"].text = cardData.effectExplain;
        texts["ToolTip"].text = cardData.toolTip;
        texts["PropertyText"].text = cardData.property.ToString();

        images["Border"].sprite = cardData.border;
        images["Property"].sprite = cardData.propertyBackground;
        images["CardIcon"].sprite = cardData.icon;
        images["PropertyIcon"].sprite = cardData.propertyIcon;

        gameObject.name = cardData.name + "SpellCard";
    }


    public void AddCard()
    {
        GameManager.Player.CardList.Add(this);
    }
    public void RemoveCard()
    {
        GameManager.Player.CardList.Remove(this);
    }
    public void UseCard(int i, Transform effectPos, Transform targetPos)   // i = 손패의 위치
    {
        if (GameManager.Player.CurMana < cardData.useMana)
            return;
        GameManager.Player.OnUseMana(cardData.useMana);
        // 카드 사용
        GameManager.Player.Grave.Enqueue(this);
        GameManager.Player.Draw(i);
    }


    private void BindChildren()
    {
        texts = new Dictionary<string, TMP_Text>();
        images = new Dictionary<string, Image>();

        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in children)
        {
            string key = child.gameObject.name;

            TMP_Text text = child.GetComponent<TMP_Text>();
            if (text != null)
                texts.Add(key, text);

            Image image = child.GetComponent<Image>();
            if (image != null)
                images.Add(key, image);
        }
    }
}
