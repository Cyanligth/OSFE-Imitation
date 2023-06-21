using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneUI : BaseUI
{
    public UnityEvent CloseDeck;
    public UnityEvent OpenMap;
    public UnityEvent Esc;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnDeck()
    {
        CloseDeck?.Invoke();
    }
    private void OnMap()
    {
        OpenMap?.Invoke();
    }
    private void OnChose()
    {

    }
    private void OnBack()
    {

    }
    private void OnEscape()
    {

    }
    private void OnUpgradeCard()
    {

    }
    private void OnRemoveCard()
    {


    }
}
