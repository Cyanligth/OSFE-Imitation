using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : BaseUI
{
    Animator animator;
    Maps maps;
    RandomMapGenerator randomMapGenerator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        maps = GetComponentInChildren<Maps>();
        randomMapGenerator = GetComponentInChildren<RandomMapGenerator>();
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
    public void DrawLine()
    {
        randomMapGenerator.DrawLine();
    }
    public void ClearLine()
    {
        randomMapGenerator.ClearLine();
    }
}
