using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class RightUI : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
        // buttons["DeckButton"].onClick.AddListener(() => { Debug.Log("asd"); });
        // 기본 텍스트 Deck
        // 상점타일 진입 시 텍스트를 Shop으로 변경
        // 상점타일 나갈 시 텍스트를 다시 Deck으로 변경
    }
}
