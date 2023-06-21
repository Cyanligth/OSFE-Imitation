using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FocusButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
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
        text.color = Color.red;
        transform.localScale = new Vector3(1.1f,1.1f,1.1f);

        ExplainPopUpUI ui = (ExplainPopUpUI)GameManager.UI.OpenPopUpUI<PopUpUI>("UI/ExplainPopUpUI");
        ui.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-3.7f, -1f));
        if(gameObject.name.LastIndexOf("1") > -1)
            ui.FocusExplain((int)GameManager.Data.CurFocus1);
        else
            ui.FocusExplain((int) GameManager.Data.CurFocus2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
        transform.localScale = Vector3.one;
        GameManager.UI.ClosePopUpUI();
    }

}
