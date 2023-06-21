using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPos;
    Vector2 aimPos;
    public UnityEvent ShuffleDeck;
    public UnityEvent<float> OnManaLow;
    Animator animator;
    CardList cardList;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        cardList = GameManager.Resource.Load<CardList>("Data/CardList");
    }
    private void Start()
    {
        aimPos = GameManager.Data.map[GameManager.Data.playerMapXY[0] + 4, GameManager.Data.playerMapXY[1]];
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
        aimPos = GameManager.Data.map[GameManager.Data.playerMapXY[0] + 4, GameManager.Data.playerMapXY[1]];
        CardData data = GameManager.Resource.Load<CardData>("Thunder");
        // if (GameManager.Player.Hand[0] == null)
        //     return;
        // if (!GameManager.Player.OnUseMana(GameManager.Player.Hand[0].cardData.useMana))
        //     return;
        if (!GameManager.Player.OnUseMana(1))
        {
            OnManaLow?.Invoke(1 - GameManager.Player.CurMana);
            return;
        }
        Attack();
        // 대충 카드의 공격 방식 ex)타게팅, 투사체, 범위공격, 맵 쓸기 등 을 만들고 그 타입에 따라 스위치로 가동
        // cardList.UseCard(cardList.Cards[0], aimPos);
        // 플레이어 덱 혹은 패에서 디큐?팝? 해야함.
        // GameManager.Player.Draw(0);

        GameManager.Resource.Instantiate<GameObject>("Effect/Thunder", new Vector3(aimPos.x, aimPos.y), transform.rotation);
        Debug.Log("spell 1 use");
    }
    private void OnSpell2() 
    {
        aimPos = GameManager.Data.map[GameManager.Data.playerMapXY[0] + 1, GameManager.Data.playerMapXY[1]];
        // if (GameManager.Player.Hand[1] == null)
        //     return;
        // if (!GameManager.Player.OnUseMana(GameManager.Player.Hand[1].cardData.useMana))
        //     return;
        if (!GameManager.Player.OnUseMana(2))
        {
            OnManaLow?.Invoke(2-GameManager.Player.CurMana);
            return;
        }
        Attack();
        // GameManager.Player.Draw(1);
        GameManager.Resource.Instantiate<GameObject>("Effect/KineticWave", new Vector3(aimPos.x, aimPos.y), transform.rotation);
        Debug.Log("spell 2 use");
    }
    private void OnShuffle()
    {
        if(!GameManager.Player.isShuffling)
        {
            GameManager.Player.Shuffle();
            ShuffleDeck?.Invoke();
        }
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
            yield return new WaitForSeconds(0.15f);
        }
    }
}
