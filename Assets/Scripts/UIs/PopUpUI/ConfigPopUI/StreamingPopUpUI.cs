using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamingPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();
        buttons["StreamingBackButton"].onClick.AddListener(() => { Close(); });
    }
    
    private void Close()
    {
        GameManager.UI.ClosePopUpUI();
    }
}
