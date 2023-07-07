using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Color color = new Color(1, 0.195f, 0.39f);
    TMP_Text text;
    RectTransform rect;
    TitleButton buttons;
    
    protected virtual void Awake()
    {
        text = GetComponent<TMP_Text>();
        rect = GetComponent<RectTransform>();
        buttons = GetComponentInParent<TitleButton>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        buttons.MoveLine(rect.position);
        transform.localScale = new Vector3(1.1f, 1.1f);
        text.color = color;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        text.color = Color.white;
    }
}
