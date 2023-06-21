using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : BaseUI
{
    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
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
    }
}
