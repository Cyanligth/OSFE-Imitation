using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPopUpUI : PopUpUI
{
    private Animator animator;
    private bool deckPopUpActive;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        // buttons["DeckButton"].onClick.AddListener(() => { deckPopUpActive = !deckPopUpActive; animator.SetBool("Active", deckPopUpActive); });
    }

    private void Start()
    {
        animator.SetBool("Active", false);
    }
    public void PopUpDeck()
    {
        deckPopUpActive = !deckPopUpActive; animator.SetBool("Active", deckPopUpActive);
    }
}
