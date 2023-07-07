using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Color color = new Color(1, 0.195f, 0.39f);
    TMP_Text text;
    ConfigPopUpUI configUI;
    RectTransform rect;
    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        configUI = GetComponentInParent<ConfigPopUpUI>();
        rect = GetComponent<RectTransform>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.UI.OpenPopUpUI<PopUpUI>("UI/SettingPopUpUI");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        configUI.MoveLines(rect.position);
        transform.localScale = new Vector3(1.1f, 1.1f);
        text.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        text.color = Color.white;
    }
}