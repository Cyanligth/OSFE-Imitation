using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TxtFollowPlayer : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void LateUpdate()
    {
        texts["PlayerFollowingHpText"].text = GameManager.Player.CurHp.ToString();
    }
}
