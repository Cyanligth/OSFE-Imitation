using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, ISpellEventListener
{
    [SerializeField] Transform attackPos;
    Vector2 aimPos;
    public UnityEvent ShuffleDeck;
    public UnityEvent<float> OnManaLow;
    public UnityEvent IsSuffling;
    public UnityEvent<CardData> UseCard;
    Animator animator;
    CardList cardList;
    AttackRoutineData attackData;
    EventMaster master;
    private void Awake()
    {
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
        animator = GetComponent<Animator>();
        cardList = GameManager.Resource.Load<CardList>("Data/CardList");
        attackData = GameManager.Resource.Load<AttackRoutineData>("Data/AttackRoutineData");
    }
    private void Start()
    {
        aimPos = GameManager.Data.map[GameManager.Data.playerXPos + 4, GameManager.Data.playerYPos];
        
    }
    private void OnEnable()
    {
        master.AddSpellEventListener(this);
    }
    private void OnDisable()
    {
        master.RemoveSpellEventListener(this);
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
        }
    }
    private int ccc = 0;

    private void OnSpell1()
    {
        if (GameManager.Player.Hand[0] == null && GameManager.Player.Hand[1] == null)
        {
            GameManager.Player.Shuffle();
            return;
        }
        if (GameManager.Player.Hand[0] == default)
        {
            OnSpell2();
            return;
        }
        if (!GameManager.Player.OnUseMana(GameManager.Player.Hand[0].useMana))
        {
            OnManaLow?.Invoke(GameManager.Player.Hand[0].useMana - GameManager.Player.CurMana);
            return;
        }
        Attack();
        GameManager.Resource.Instantiate<GameObject>(GameManager.Player.Hand[0].cardEffect);
        GameManager.Player.Grave.Enqueue(GameManager.Player.Hand[0]);
        GameManager.Player.Draw(0);
        master.Hand1UseInvoke();
        Debug.Log("spell 1 use");
    }
    private void OnSpell2() 
    {
        if (GameManager.Player.Hand[0] == default && GameManager.Player.Hand[1] == default)
        {
            GameManager.Player.Shuffle();
            return;
        }
        if (GameManager.Player.Hand[1] == default)
        {
            OnSpell1();
            return;
        }
        if (!GameManager.Player.OnUseMana(GameManager.Player.Hand[1].useMana))
        {
            OnManaLow?.Invoke(GameManager.Player.Hand[1].useMana - GameManager.Player.CurMana);
            return;
        }
        Attack();
        GameManager.Resource.Instantiate<GameObject>(GameManager.Player.Hand[1].cardEffect);
        GameManager.Player.Grave.Enqueue(GameManager.Player.Hand[1]);
        GameManager.Player.Draw(1);
        master.Hand2UseInvoke();
        Debug.Log("spell 2 use");
    }
    private void OnShuffle()
    {
        if(!GameManager.Player.isShuffling)
        {
            GameManager.Player.ShuffleHand();
        }
        else
        {
            IsSuffling?.Invoke();
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

    public void Hand1UseEvent()
    {
        
    }

    public void Hand2UseEvent()
    {
        
    }

    public void ShuffleEvent()
    {
        ShuffleDeck.Invoke();
    }

    public void GetNewCardEvent(CardData card)
    {
    }

    public void DeleteCardEvent(CardData card)
    {
    }
}
