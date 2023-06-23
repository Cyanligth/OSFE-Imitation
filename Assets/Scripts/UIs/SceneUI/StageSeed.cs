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
        texts["SeedText"].text = "Seed: " + GameManager.Data.seed.ToString(); // 이번 맵의 시드
    }

    public void ChangeSeed()
    {
        texts["SeedText"].text = "Seed: " + GameManager.data.seed.ToString();
    }
    public void ChangeStage()
    {
        texts["StageText"].text = "1"/*몇번째 월드인지*/ + " - " + "2"/*월드 내의 몇번째 스테이지*/ + "(" + "Shiso"/*월드 이름*/ + ")";
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
