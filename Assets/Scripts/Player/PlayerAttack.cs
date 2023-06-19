using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPos;
    [SerializeField] Transform aimPos;
    Animator animator;
    CardList cardList;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardList = GameManager.Resource.Load<CardList>("Data/CardList");
    }
    private void Attack()
    {
        if (!animator.GetBool("Attack1") && !animator.GetBool("Attack2") && !animator.GetBool("Attack3"))
        {
            StartCoroutine(Attack1Counting());
        }
        else if (animator.GetBool("Attack1") && !animator.GetBool("Attack2"))
        {
            StartCoroutine(Attack2Counting());
        }
        else if (animator.GetBool("Attack2") && !animator.GetBool("Attack3"))
        {
            StartCoroutine(Attack3Counting());
        }
        else if (animator.GetBool("Attack3") && !animator.GetBool("Attack1"))
        {
            StartCoroutine(Attack1Counting());
        }
    }
    private void OnAttack(InputValue input)
    {
        ccc = (ccc + 1) % 2;
        if (ccc % 2 != 0)
        {
            StartCoroutine(Rapid());
            Debug.Log("rapid");
        }
        else
        {
            StopCoroutine(Rapid());
            StopCoroutine(Attack1Counting());
            StopCoroutine(Attack2Counting());
            StopCoroutine(Attack3Counting());
            StopAllCoroutines();
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", false);
            Debug.Log("end");
        }
    }
    private int ccc = 0;

    private void OnSpell1()
    {
        Attack();
        GameManager.Player.OnUseMana(1f);
        // cardList.UseCard(cardList.Cards[0], aimPos);
        Debug.Log("spell 1 use");
        // 플레이어 덱 혹은 패에서 디큐?팝? 해야함.
    }
    private void OnSpell2() 
    {
        Attack();
        Debug.Log("spell 2 use");
    }

    private void CreatBullet()
    {
        GameManager.Resource.Instantiate<BasicCast>("Effect/BasicCast", attackPos.position, attackPos.rotation);
        GameManager.Resource.Instantiate<BasicSpell>("Effect/BasicSpell", attackPos.position, attackPos.rotation); 
    }

    IEnumerator Attack1Counting()
    {
        animator.SetBool("Attack1", true);
        // CreatBullet();
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack1", false);
    }
    IEnumerator Attack2Counting()
    {
        animator.SetBool("Attack2", true);
        // CreatBullet();
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack2", false);
    }
    IEnumerator Attack3Counting()
    {
        animator.SetBool("Attack3", true);
        // CreatBullet();
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack3", false);
    }
    IEnumerator Rapid()
    {
        while(true) 
        {
            Attack();
            CreatBullet();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
