using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigEventListener
{
    public abstract void OpenConfigEvent();
    public abstract void CloseConfigEvent();
}
