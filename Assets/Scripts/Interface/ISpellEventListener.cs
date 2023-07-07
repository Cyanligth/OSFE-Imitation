using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpellEventListener
{
    public abstract void Hand1UseEvent();
    public abstract void Hand2UseEvent();
    public abstract void ShuffleEvent();
    public abstract void GetNewCardEvent(CardData card);
    public abstract void DeleteCardEvent(CardData card);
}
