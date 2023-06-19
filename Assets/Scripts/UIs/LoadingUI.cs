using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : BaseUI
{
    Animator animator;
    Slider slider;
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        slider = GetComponentInChildren<Slider>();
    }
    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        animator.SetBool("Active", true);
    }
    public void FadeOut()
    {
        animator.SetBool("Active", false);
    }

    public void SetProgress(float value)
    {
        slider.value = value;
    }
}
