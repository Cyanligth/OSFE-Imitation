using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DeckPopUpUI : BaseUI, IDeckEventListener, IConfigEventListener
{
    private Animator animator;
    PlayerInput input;
    PlayerStatUI playerStat;
    EventMaster master;
    bool isDeckOpen;

    protected override void Awake()
    {
        base.Awake();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        playerStat = GetComponentInChildren<PlayerStatUI>();
        images["Mask"].enabled = false;
        animator.SetBool("Active", false);
        input.enabled = false;
        
        // buttons["DeckButton"].onClick.AddListener(() => { deckPopUpActive = !deckPopUpActive; animator.SetBool("Active", deckPopUpActive); });
    }
    private void OnEnable()
    {
        master.AddDeckEventListener(this);
        master.AddConfigEventListener(this);
    }
    private void OnDisable()
    {
        master.RemoveDeckEventListener(this);
        master.RemoveConfigEventListener(this);
    }
    private void OnDeck()
    {
        master.CloseDeckInvoke();
    }
    private void OnBack()
    {
        master.CloseDeckInvoke();
    }
    private void OnUpgradeCard(InputValue value)
    {
        // 업그레이드가 있다면 선택한 카드 홀드로 업그레이드
    }
    private void OnDeleteCard(InputValue value)
    {
        // 지우기가 있다면 선택한 카드 홀드로 지우기
    }
    private void OnEscape()
    {
        GameManager.UI.OpenPopUpUI<ConfigPopUpUI>("UI/ConfigPopUpUI");
        master.OpenConfigInvoke();
    }
    public void EnableMask()
    {
        images["Mask"].enabled = true;
    }
    public void DisableMask() 
    {
        images["Mask"].enabled = false;
    }
    public void OpenDeckEvent()
    {
        isDeckOpen = true;
        playerStat.SetStat();
        animator.SetBool("Active", true);
        input.enabled = true;
    }
    public void CloseDeckEvent()
    {
        isDeckOpen = false;
        animator.SetBool("Active", false);
        input.enabled = false;
    }

    public void OpenConfigEvent()
    {
        if(isDeckOpen) input.enabled = false;
    }

    public void CloseConfigEvent()
    {
        if(isDeckOpen) input.enabled = true;
    }
}
