using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FocusPopUpUI2 : BaseUI
{
    FocusData focus;

    public UnityEvent ChangeFocus;
    Animator animator;
    private bool focusActive;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        focus = GameManager.Resource.Load<FocusData>("Data/FocusData");
        for (int i = 0; i < 11; i++)
        {
            texts[$"ChoseFocusText2-{i}"].text = focus.property[i].ToString();
            images[$"FocusChoseIcon2-{i}"].sprite = focus.FocusIcons[i];
        }
        ChoseFocus[] choseFocus = GetComponentsInChildren<ChoseFocus>();
        foreach (ChoseFocus chose in choseFocus)
            chose.FocusOnChange.AddListener(() => { ChangeFocus?.Invoke(); PopUpFocus2(); });
    }

    public void PopUpFocus2()
    {
        focusActive = !focusActive; animator.SetBool("Active", focusActive);
    }
    public void EnableUI()
    {
        gameObject.SetActive(true);
    }
    public void DisableUI()
    {
        gameObject.SetActive(false);
    }
}
