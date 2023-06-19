using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        texts["PlayerName"].text = GameManager.Player.Job;
    }
    private void Update()
    {
        // ���߿� �̺�Ʈ�� �����ϱ�
        texts["PlayerLevel"].text = "Level " + GameManager.Player.Level.ToString();
        texts["MoneyTxt"].text = GameManager.Player.Money.ToString();
        texts["LifeTxt"].text = GameManager.player.CurHp.ToString() + "/" + GameManager.Player.MaxHp.ToString();
    }
}
