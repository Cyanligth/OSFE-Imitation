using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCast : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        animator.Play("BasicCast");
        GameManager.Resource.Destroy(gameObject, 0.1f);
    }
}
