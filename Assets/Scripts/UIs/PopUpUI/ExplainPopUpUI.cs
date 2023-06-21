using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplainPopUpUI : PopUpUI
{
    TMP_Text text;
    FocusData focus;
    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TMP_Text>();
        focus = GameManager.Resource.Load<FocusData>("Data/FocusData");
    }

    public void FocusExplain(int i)
    {
        text.text = focus.FocusText[i];
    }
}
