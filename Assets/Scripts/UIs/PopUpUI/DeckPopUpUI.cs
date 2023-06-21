using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckPopUpUI : BaseUI
{
    private Animator animator;
    private bool deckPopUpActive;
    public UnityEvent DeckOpened;
    public UnityEvent DeckClosed;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        images["Mask"].enabled = false;
        animator.SetBool("Active", false);
        // buttons["DeckButton"].onClick.AddListener(() => { deckPopUpActive = !deckPopUpActive; animator.SetBool("Active", deckPopUpActive); });
    }
    public void PopUpDeck()
    {
        deckPopUpActive = !deckPopUpActive; animator.SetBool("Active", deckPopUpActive);
        if (deckPopUpActive ) { DeckOpened?.Invoke(); }
        else { DeckClosed?.Invoke(); }
    }
    public void EnableMask()
    {
        images["Mask"].enabled = true;
    }
    public void DisableMask() 
    {
        images["Mask"].enabled = false;
    }
}
