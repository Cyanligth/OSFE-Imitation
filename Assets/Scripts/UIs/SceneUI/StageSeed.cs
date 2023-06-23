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
        texts["SeedText"].text = "Seed: " + GameManager.Data.seed.ToString(); // �̹� ���� �õ�
    }

    public void ChangeSeed()
    {
        texts["SeedText"].text = "Seed: " + GameManager.data.seed.ToString();
    }
    public void ChangeStage()
    {
        texts["StageText"].text = "1"/*���° ��������*/ + " - " + "2"/*���� ���� ���° ��������*/ + "(" + "Shiso"/*���� �̸�*/ + ")";
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
