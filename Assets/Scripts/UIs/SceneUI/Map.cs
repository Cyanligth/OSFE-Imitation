using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Map : BaseUI, IDeckEventListener, IMapEventListener, IConfigEventListener, IStageEventListener
{
    Animator animator;
    RectTransform rect;
    RandomMapGenerator randomMapGenerator;
    LocationMarker marker;
    EventMaster master;
    PlayerInput input;
    public UnityEvent OnNextStage; 
    public UnityEvent OnNextWorld;
    public UnityEvent OnDraw;
    bool isMapOpen;

    protected override void Awake()
    {
        base.Awake();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
        input = GetComponent<PlayerInput>();
        rect = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        marker = GetComponentInChildren<LocationMarker>();
        randomMapGenerator = GetComponentInChildren<RandomMapGenerator>();
        input.enabled = false;
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
    private void Start()
    {
        foreach (Node node in randomMapGenerator.nodeList)
        {
            node.MouseOn.AddListener((a) => { ChangeLocation(a); }); //
            node.OnClick.AddListener((a) => { NextStage(a); }); // 다음스테이지로 가는 이벤트
        }
    }
    private bool onOpen;
    public void OpenMap()
    {
        onOpen = !onOpen;
        if (onOpen)
        {
            animator.SetBool("OnOpen", true);
        }
        else
        {
            animator.SetBool("OnOpen", false);
        }
    } // 450, -170
    public void DrawLine()
    {
        OnDraw?.Invoke();
        randomMapGenerator.DrawLine();
        images["CurrentLocation"].transform.position = randomMapGenerator.curNode.rect.position;
        images["LocationMarker"].transform.position = randomMapGenerator.curNode.nextRoom[0].rect.position;
        marker.SetStage(randomMapGenerator.curNode.curStage+1);
        Vector3 v = images["LocationMarker"].transform.position - images["CurrentLocation"].transform.position;
        float f = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        images["CurrentLocation"].transform.rotation = Quaternion.Euler(0, 0, f);
    }
    public void ClearLine()
    {
        randomMapGenerator.ClearLine();
    }
    public void ChangeLocation(Node node)
    {
        if (!randomMapGenerator.curNode.nextRoom.Contains(node))
            return;
        marker.Move(node.rect.position);
        images["CurrentLocation"].transform.position = randomMapGenerator.curNode.rect.position;
        Vector3 v = node.rect.position - images["CurrentLocation"].transform.position;
        float f = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        images["CurrentLocation"].transform.rotation = Quaternion.Euler(0, 0, f);
    }
    public void NextStage(Node node)
    {
        if (!randomMapGenerator.curNode.nextRoom.Contains(node))
            return;
        randomMapGenerator.curNode = node;
        if (node.curStage < 7)
        {
            master.NextStageInvoke($"{GameManager.Data.worldCount}-{node.curStage+1}");
        }
        else
        {
            master.NextWorldInvoke();
            GameManager.Data.worldCount++;
        }
        
        // 다음월드 진입
    }

    public void OpenDeckEvent() { if (isMapOpen) { input.enabled = false; } else return; }
    public void CloseDeckEvent() { if (isMapOpen) { input.enabled = true; } else return; }

    public void OpenMapEvent()
    {
        animator.SetBool("OnOpen", true);
        isMapOpen = true;
        input.enabled = true;
    }
    public void CloseMapEvent()
    {
        isMapOpen = false;
        input.enabled = false;
        animator.SetBool("OnOpen", false);
    }
    public void NextStageEvent(string nextStage)
    {
        isMapOpen = false;
        input.enabled = false;
        animator.SetBool("OnOpen", false);
        OnNextStage?.Invoke();
    }
    private void OnMap()
    {
        master.CloseMapInvoke();
        isMapOpen = false;
    }
    private void OnDeck()
    {
        master.OpenDeckInvoke();
    }
    private void OnBack()
    {
        master.CloseMapInvoke();
        isMapOpen= false;
    }
    private void OnChose(InputValue value)
    {

    }
    private void OnEscape()
    {
        GameManager.UI.OpenPopUpUI<ConfigPopUpUI>("UI/ConfigPopUpUI");
        master.OpenConfigInvoke();
    }

    public void OpenConfigEvent()
    {
        if(isMapOpen)
            input.enabled = false;
    }

    public void CloseConfigEvent()
    {
        if(isMapOpen)
            input.enabled = true;
    }

    public void GameStartEvent()
    {
        
    }

    public void NextWorldEvent()
    {
        
    }

    public void StageClearEvent()
    {
        
    }

    public void ReadyToBattleEvent()
    {
        
    }
}
