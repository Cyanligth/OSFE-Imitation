using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class PlayerToUI : MonoBehaviour, IDeckEventListener, IMapEventListener, IConfigEventListener, IStageEventListener
{
    public UnityEvent Test;
    PlayerInput input;
    PlayerMover mover;
    EventMaster master;
    bool isMapOpen;
    bool isDeckOpen;
    private void Awake()
    {
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
        input = GetComponent<PlayerInput>();
        mover = GetComponent<PlayerMover>();
        isMapOpen = false;
        isDeckOpen = false;
    }
    private void OnEnable()
    {
        master.AddDeckEventListener(this);
        master.AddMapEventListener(this);
        master.AddConfigEventListener(this);
        master.AddStageEventListener(this);
    }
    private void OnDisable()
    {
        master.RemoveDeckEventListener(this);
        master.RemoveMapEventListener(this);
        master.RemoveConfigEventListener(this);
        master.RemoveStageEventListener(this);
    }

    public void CloseDeckEvent()
    {
        if (isMapOpen)
        {
            isDeckOpen = false;
            return;
        }
        input.enabled = true;
        mover.enabled = true;
        isDeckOpen = false;
    }

    public void CloseMapEvent()
    {
        isMapOpen = false;
        input.enabled = true;
        mover.enabled = true;
    }

    public void OpenDeckEvent()
    {
        isDeckOpen = true;
        input.enabled = false;
        mover.enabled = false;
    }

    public void OpenMapEvent()
    {
        isMapOpen = true;
        input.enabled = false;
        mover.enabled = false;
    }

    private void OnDeck()
    {
        master.OpenDeckInvoke();
    }
    private void OnEscape()
    {
        GameManager.UI.OpenPopUpUI<ConfigPopUpUI>("UI/ConfigPopUpUI");
        master.OpenConfigInvoke();
    }
    private void OnMap()
    {
        if(GameManager.Data.IsClearThisStage)
            master.OpenMapInvoke();
    }
    private void OnTest()
    {
        Test?.Invoke();
    }

    public void OpenConfigEvent()
    {
        input.enabled = false;
        mover.enabled = false;
    }

    public void CloseConfigEvent()
    {
        if (isDeckOpen || isMapOpen)
            return;
        input.enabled = true;
        mover.enabled = true;
    }

    public void GameStartEvent()
    {
        GameManager.Player.ShuffleHand();
    }

    public void NextWorldEvent()
    {
        GameManager.Data.IsClearThisStage = false;
        input.enabled = true;
        mover.enabled = true;
        isDeckOpen = false;
        isMapOpen = false;
    }

    public void NextStageEvent(string nextStage)
    {
        GameManager.Data.IsClearThisStage = false;
        input.enabled = false;
        mover.enabled = false;
        isDeckOpen = false;
        isMapOpen = false;
        mover.OnNextStage(2);
    }

    public void StageClearEvent()
    {
        GameManager.Data.IsClearThisStage = true;
        GameManager.player.Luck++;
    }

    public void ReadyToBattleEvent()
    {
        input.enabled = true;
        mover.enabled = true;
    }
}
