using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FocusButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color none;
    [SerializeField] Color onMouse;

    public UnityEvent focusButtonClick;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        focusButtonClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = onMouse;
        transform.localScale = new Vector3(1.1f,1.1f,1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = none;
        transform.localScale = Vector3.one;
    }
}
