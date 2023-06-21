using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSeed : BaseUI
{
    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        animator.SetBool("IsMapOpen", false);
    }

    private void Start()
    {
        texts["SeedText"].text = "Seed: " + "13579284"; // 이번 맵의 시드
    }

    public void StageChange()
    {
        texts["StageText"].text = "1"/*몇번째 월드인지*/ + " - " + "2"/*월드 내의 몇번째 스테이지*/ + "(" + "Shiso"/*스테이지 이름?*/ + ")";
    }

    private bool isMapOpen;
    public void MovePos()
    {
        isMapOpen = !isMapOpen;
        if (isMapOpen)
        {
            animator.SetBool("IsMapOpen", true);
        }
        else
        {
            animator.SetBool("IsMapOpen", false);
        }
    }
}
