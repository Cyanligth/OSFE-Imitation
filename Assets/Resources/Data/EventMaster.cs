using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.FlowStateWidget;

[CreateAssetMenu(fileName = "EventMaster", menuName = "Data/Event")]
public class EventMaster : ScriptableObject
{
    public List<IDeckEventListener> DeckEventListeners = new List<IDeckEventListener>();
    public List<IMapEventListener> MapEventListeners = new List<IMapEventListener>();
    public List<IConfigEventListener> ConfigEventListeners = new List<IConfigEventListener>();
    public List<IStageEventListener> StageEventListeners = new List<IStageEventListener>();
    public List<ISpellEventListener> SpellEventListeners = new List<ISpellEventListener>();

    public void OpenDeckInvoke()
    {
        foreach (var listener in DeckEventListeners)
            listener?.OpenDeckEvent();
    }
    public void CloseDeckInvoke()
    {
        foreach (var listener in DeckEventListeners)
            listener?.CloseDeckEvent();
    }
    public void OpenMapInvoke()
    {
        foreach (var listener in MapEventListeners)
            listener?.OpenMapEvent();
    }
    public void CloseMapInvoke() 
    {
        foreach (var listener in MapEventListeners)
            listener?.CloseMapEvent();
    }
    public void NextStageInvoke(string curStage)
    {
        foreach (var listener in StageEventListeners)
            listener?.NextStageEvent(curStage);
    }
    public void GameStartInvoke()
    {
        foreach (var listener in StageEventListeners)
            listener?.GameStartEvent();
    }
    public void NextWorldInvoke()
    {
        foreach (var listener in StageEventListeners)
            listener?.NextWorldEvent();
    }
    public void StageClearInvoke()
    {
        foreach (var listener in StageEventListeners)
            listener?.StageClearEvent();
    }
    public void ReadyToBattleInvoke()
    {
        foreach (var listener in StageEventListeners)
            listener?.ReadyToBattleEvent();
    }
    public void OpenConfigInvoke()
    {
        foreach (var listener in ConfigEventListeners)
            listener?.OpenConfigEvent();
    }
    public void CloseConfigInvoke()
    {
        foreach (var listener in ConfigEventListeners)
            listener?.CloseConfigEvent();
    }
    public void Hand1UseInvoke()
    {
        foreach (var listener in SpellEventListeners)
            listener?.Hand1UseEvent();  
    }
    public void Hand2UseInvoke()
    {
        foreach (var listener in SpellEventListeners)
            listener?.Hand2UseEvent();
    }
    public void ShuffleInvoke()
    {
        foreach(var listener in SpellEventListeners)
            listener?.ShuffleEvent();
    }
    public void GetNewCardInvoke(CardData card)
    {
        foreach (var listener in SpellEventListeners)
            listener?.GetNewCardEvent(card);
    }
    public void DeleteCardInvoke(CardData card)
    {
        foreach (var listener in SpellEventListeners)
            listener?.DeleteCardEvent(card);
    }

    public void AddDeckEventListener(IDeckEventListener listener)
    {
        DeckEventListeners.Add(listener);
    }
    public void RemoveDeckEventListener(IDeckEventListener listener)
    {
        DeckEventListeners.Remove(listener);
    }
    public void AddMapEventListener(IMapEventListener listener)
    {
        MapEventListeners.Add(listener);
    }
    public void RemoveMapEventListener(IMapEventListener listener)
    {
        MapEventListeners.Remove(listener);
    }
    public void AddConfigEventListener(IConfigEventListener listener)
    {
        ConfigEventListeners.Add(listener);
    }
    public void RemoveConfigEventListener(IConfigEventListener listener)
    {
        ConfigEventListeners.Remove(listener);
    }
    public void AddStageEventListener(IStageEventListener listener)
    { 
        StageEventListeners.Add(listener); 
    }
    public void RemoveStageEventListener(IStageEventListener listener)
    {
        StageEventListeners.Remove(listener);
    }
    public void AddSpellEventListener(ISpellEventListener listener)
    { 
        SpellEventListeners.Add(listener);
    }
    public void RemoveSpellEventListener(ISpellEventListener listener)
    { 
        SpellEventListeners.Remove(listener);
    }
}
