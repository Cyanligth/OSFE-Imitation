using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FocusPopUpUI1 : BaseUI
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
        for(int i = 0; i < 11; i++)
        {
            texts[$"ChoseFocusText1-{i}"].text = focus.property[i].ToString();
            images[$"FocusChoseIcon1-{i}"].sprite = focus.FocusIcons[i];
        }
        ChoseFocus[] choseFocus = GetComponentsInChildren<ChoseFocus>();
        foreach (ChoseFocus chose in choseFocus)
            chose.FocusOnChange.AddListener(() => { ChangeFocus?.Invoke(); PopUpFocus1(); });
    }

    public void PopUpFocus1()
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
