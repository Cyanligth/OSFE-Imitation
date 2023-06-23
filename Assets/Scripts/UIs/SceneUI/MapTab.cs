using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTab : MonoBehaviour
{
    Animator animator;
    bool isActive;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeState()
    {
        isActive = !isActive;
        animator.SetBool("Active", isActive);
    }

    public void EnableMapTap()
    {
        gameObject.SetActive(true);
        animator.SetBool("Active", true);
    }
    public void DisableMapTab()
    {
        gameObject.SetActive(false);
    }
}
