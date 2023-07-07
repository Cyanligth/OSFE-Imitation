using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfigPopUpUI : PopUpUI, IConfigEventListener
{
    Animator animator;
    EventMaster master;
    PlayerInput input;
    Lines lines;
    protected override void Awake()
    {
        base.Awake();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        lines = GetComponentInChildren<Lines>();
    }
    private void OnEnable()
    {
        master.AddConfigEventListener(this);
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        master.RemoveConfigEventListener(this);
    }
    public void CloseConfigPopUpUI()
    {
        master.CloseConfigInvoke();
        Time.timeScale = 1;
        GameManager.UI.ClosePopUpUI();
    }
    public void MoveLines(Vector2 endPos)
    {
        lines.MoveLine(endPos);
    }
    public void OpenConfigEvent()
    {
        input.enabled = true;
    }

    public void CloseConfigEvent()
    {
        input.enabled = false;
    }

    private void OnBack()
    {
        if(GameManager.UI.popUpStack.Peek() == this)
            animator.SetTrigger("Close");
        else
            GameManager.UI.ClosePopUpUI();
    }
    private void OnChose()
    {

    }
    private void OnEscape()
    {
        if (GameManager.UI.popUpStack.Peek() == this)
            animator.SetTrigger("Close");
        else
            GameManager.UI.ClosePopUpUI();
    }
}
