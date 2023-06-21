using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerToUI : MonoBehaviour
{
    public UnityEvent OpenDeck;
    public UnityEvent Esc;
    public UnityEvent Map;

    private void OnDeck()
    {
        OpenDeck?.Invoke();
    }
    private void OnEscape()
    {
        Esc?.Invoke();
    }
    private void OnMap()
    {
        Map?.Invoke();
    }
}
