using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
    }
    public void Active()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
