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
        texts["SeedText"].text = "Seed: " + "13579284"; // �̹� ���� �õ�
    }

    public void StageChange()
    {
        texts["StageText"].text = "1"/*���° ��������*/ + " - " + "2"/*���� ���� ���° ��������*/ + "(" + "Shiso"/*�������� �̸�?*/ + ")";
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
