using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTab : MonoBehaviour, IStageEventListener
{
    Animator animator;
    bool isActive;
    EventMaster master;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
    }
    private void OnEnable()
    {
        master.AddStageEventListener(this);
    }
    private void OnDisable()
    {
        master.RemoveStageEventListener(this);
    }

    public void ChangeState()
    {
        isActive = !isActive;
        animator.SetBool("Active", isActive);
    }

    public void EnableMapTap()
    {
        gameObject.SetActive(true);
        animator.SetBool("Active", true);
    }
    public void DisableMapTab()
    {
        gameObject.SetActive(false);
    }

    public void GameStartEvent()
    {
    }

    public void StageClearEvent()
    {
        // Ȱ��ȭ
    }

    public void ReadyToBattleEvent()
    {
    }

    public void NextStageEvent(string nextStage)
    {
        // ��Ȱ��ȭ
    }

    public void NextWorldEvent()
    {
        // ��Ȱ��ȭ
    }
}
