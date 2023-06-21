using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoseFocus : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent FocusOnChange;
    RectTransform rect;
    TMP_Text text;
    private int i = -1;
    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        rect = GetComponent<RectTransform>();
    
        if (gameObject.name[gameObject.name.Length - 2].ToString() == "-")
        {
            int.TryParse(gameObject.name[gameObject.name.Length - 1].ToString(), out i);
        }
        else
        {
            int.TryParse(gameObject.name[gameObject.name.Length - 2].ToString() + gameObject.name[gameObject.name.Length - 1].ToString(), out i);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 이거를 현재 집중으로 설정
        if(gameObject.name.LastIndexOf("1-") > -1)
        {
            GameManager.Data.CurFocus1 = (DataManager.Property)i;
            Debug.Log("111 : "+GameManager.Data.CurFocus1.ToString());
        }
        else if(gameObject.name.LastIndexOf("2-") > -1)
        {
            GameManager.Data.CurFocus2 = (DataManager.Property)i;
            Debug.Log("222 : "+GameManager.Data.CurFocus2.ToString());
        }
        else
        { Debug.Log("asdfasdfasdf"); }
        FocusOnChange?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.localScale = new Vector3(0.8f, 0.8f);
        // 글자색 붉게+팝업유아이로 설명문 띄우기
        text.color = Color.red;

        ExplainPopUpUI ui = (ExplainPopUpUI)GameManager.UI.OpenPopUpUI<PopUpUI>("UI/ExplainPopUpUI");
        ui.FocusExplain(i);
        if (gameObject.name.LastIndexOf("1-") > -1)
        {
            ui.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-5.18f, -0.48f));
        }
        else
        {
            ui.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-4f, -0.48f));
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 팝업유아이 지우고 글자색 원래대로
        rect.localScale = new Vector3(0.7f, 0.7f);
        text.color = Color.white;
        GameManager.UI.ClosePopUpUI();
    }
}
