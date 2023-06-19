using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerToUI : MonoBehaviour
{
    public UnityEvent OpenDeck;
    public UnityEvent ShuffleDeck;
    public UnityEvent Esc;

    private void OnDeck()
    {
        OpenDeck?.Invoke();
    }
    private void OnShuffle()
    {
        ShuffleDeck?.Invoke();
    }
    private void OnEscape()
    {
        Esc?.Invoke();
    }
}
