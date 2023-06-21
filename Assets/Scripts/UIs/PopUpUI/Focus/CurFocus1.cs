using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurFocus1 : BaseUI
{
    FocusData focus;
    Image image;
    TMP_Text text;
    protected override void Awake()
    {
        base.Awake();
        focus = GameManager.Resource.Load<FocusData>("Data/FocusData");
        text = GetComponentInChildren<TMP_Text>();
        image = GetComponent<Image>();
    }

    public void Focus1OnChanged()
    {
        text.text = focus.property[((int)GameManager.Data.CurFocus1)].ToString();
        image.sprite = focus.FocusIcons[((int)GameManager.Data.CurFocus1)];
    }
}
