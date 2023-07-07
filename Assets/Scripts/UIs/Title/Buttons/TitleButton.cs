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
        // 만약 저장된 파일이 있다면 버튼을 활성화
        line = GetComponentInChildren<TitleLine>();
    }
    public void MoveLine(Vector2 endPos)
    {
        line.Move(endPos);
    }
}
