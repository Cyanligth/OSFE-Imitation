using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : BaseUI
{
    TitleLine line;
    protected override void Awake()
    {
        base.Awake();
        texts["ContinueRun"].gameObject.SetActive(false);
        // ���� ����� ������ �ִٸ� ��ư�� Ȱ��ȭ
        line = GetComponentInChildren<TitleLine>();
    }
    public void MoveLine(Vector2 endPos)
    {
        line.Move(endPos);
    }
}
