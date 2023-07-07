using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : BaseNpc
{
    Animator animator;
    bool isOpen;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        // 이벤트 받기
    }
    protected override void Start()
    {
        base.Start();
        transform.position = GameManager.Data.map[7,2];
    }
    private void OnEnable()
    {
        Hp = 3000;
    }
    public override void Communicate()
    {
        isOpen = !isOpen;
        if (isOpen)
            GameManager.UI.OpenPopUpUI<PopUpUI>("UI/ShopPopUpUI");
        else
            GameManager.UI.ClosePopUpUI();
    }

    protected override void IsHited()
    {
        
    }
}
