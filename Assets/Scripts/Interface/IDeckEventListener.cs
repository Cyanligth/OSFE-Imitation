using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDeckEventListener
{
    public abstract void OpenDeckEvent();
    public abstract void CloseDeckEvent();
}
