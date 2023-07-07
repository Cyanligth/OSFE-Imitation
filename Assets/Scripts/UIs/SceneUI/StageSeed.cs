using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSeed : BaseUI, IMapEventListener, IStageEventListener
{
    Animator animator;
    EventMaster master;
    protected override void Awake()
    {
        base.Awake();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
        animator = GetComponent<Animator>();
        animator.SetBool("IsMapOpen", false);
    }
    private void OnEnable()
    {
        master.AddMapEventListener(this);
        master.AddStageEventListener(this);
    }
    private void OnDisable()
    {
        master.RemoveMapEventListener(this);
        master.RemoveStageEventListener(this);
    }

    private void Start()
    {
        texts["SeedText"].text = "Seed: " + GameManager.Data.seed.ToString(); // �̹� ���� �õ�
        texts["StageText"].text = $"{GameManager.Data.worldCount}-1";
    }

    public void ChangeSeed()
    {
        texts["SeedText"].text = "Seed: " + GameManager.data.seed.ToString();
    }
    public void ChangeStage()
    {
        texts["StageText"].text = "1"/*���° ��������*/ + " - " + "2"/*���� ���� ���° ��������*/ + "(" + "Shiso"/*���� �̸�*/ + ")";
    }
    public void OpenMapEvent()
    {
        animator.SetBool("IsMapOpen", true);
    }
    public void CloseMapEvent()
    {
        animator.SetBool("IsMapOpen", false);
    }

    public void GameStartEvent()
    {
        
    }

    public void NextStageEvent(string curStage)
    {
        animator.SetBool("IsMapOpen", false);
        texts["StageText"].text = curStage;
    }

    public void NextWorldEvent()
    {
        texts["StageText"].text = $"{GameManager.Data.worldCount}-1";
    }

    public void StageClearEvent()
    {
        
    }

    public void ReadyToBattleEvent()
    {
    }
}
