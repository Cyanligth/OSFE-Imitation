using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapEventListener
{
    public abstract void OpenMapEvent();
    public abstract void CloseMapEvent();
}
