using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    protected int level;
    protected int maxExp;
    protected int curExp;
    protected string job;
    protected string weapon;
    protected int maxHp;
    protected int curHp;
    protected int atk;
    protected int matk;
    protected int luck;
    protected int shield;
    protected float maxMana;
    protected float curMana;
    protected float manaRegen;
    protected int money;
    protected Stack<CardData> deck;
    protected Queue<CardData> grave;
    protected CardData[] hand = new CardData[2];
    protected List<CardData> cardList;
    protected Vector2 playerPos;

    public bool anchor;

    EventMaster master;

    public int Level { get { return level; } set { level = value; } }
    public int MaxExp { get { return maxExp; } set { maxExp = value; } }
    public int CurExp { get { return curExp; } set { curExp = value; } }
    public string Job { get { return job; } set { job = value; } }
    public string Weapon { get { return weapon; } set { weapon = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int CurHp { get { return curHp; } set { curHp = value; } }
    public int Atk { get { return atk; } set { atk = value; } }
    public int Matk { get { return matk; } set { matk = value; } }
    public int Luck { get { return luck; } set { luck = value; } }
    public int Shield { get { return shield; } set { shield = value; } }
    public float MaxMana { get { return maxMana; } set { maxMana = value; } }
    public float CurMana { get { return curMana; } set { curMana = value; } }
    public float ManaRegen { get { return manaRegen; } set { manaRegen = value; } }
    public int Money { get { return money; } set { money = value; } }

    public Vector2 PlayerPos { get { return playerPos; } set { playerPos = value; } }
    public Stack<CardData> Deck { get { return deck; } private set { deck = value; } }
    public Queue<CardData> Grave { get { return grave; } private set { grave = value; } }
    public CardData[] Hand { get { return hand; } private set { hand = value; } }
    public List<CardData> CardList { get { return cardList; } private set { cardList = value; } }
    public void GetNewCard(CardData card)
    {
        master.GetNewCardInvoke(card);
        cardList.Add(card);
        deck.Push(card);
        manaRegen = 0.5f + (cardList.Count/5);
    }

    private void Awake()
    {
        cardList = new List<CardData>();
        deck = new Stack<CardData>();
        grave = new Queue<CardData>();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
    }

    public void Draw(int i)
    {
        hand[i] = null;
        if (deck.Count > 0)
            hand[i] = deck.Pop();
        else if (hand[0] == null && hand[1] == null)
        {
            Shuffle();
        }
        else
            return;
    }
    public void Shuffle()
    {
        StartCoroutine(Shuffling(1f));
        CardData[] cards = new CardData[grave.Count];
        while(deck.Count > 0)
        {
            grave.Enqueue(deck.Pop());
        }
        while(grave.Count > 0)
        {
            int i = Random.Range(0, cards.Length);
            if (cards[i] == null)
            {
                cards[i] = grave.Dequeue();
            }
        }
        foreach(CardData card in cards)
            deck.Push(card);
        Draw(0);
        Draw(1);
        master.ShuffleInvoke();
    }
    public void ShuffleHand()
    {
        if (hand[0] != null)
        {
            grave.Enqueue(hand[0]);
            hand[0] = null;
        }
        if (hand[1] != null)
        {
            grave.Enqueue(hand[1]);
            hand[1] = null;
        }
        while (deck.Count > 0)
        {
            grave.Enqueue(deck.Pop());
        }
        Shuffle();
    }
    public bool OnUseMana(float mana)
    {
        if(curMana < mana)
        {
            return false;
        }
        curMana -= mana;
        return true;
    }

    public bool isShuffling;
    IEnumerator Shuffling(float i)
    {
        GameManager.Sound.Play("shuffle_start");
        isShuffling = true;
        yield return new WaitForSeconds(i);
        isShuffling = false;
        GameManager.Sound.Play("shuffle_end");
    }
    public void Hit(int damage)
    {
        if (shield > 0)
            shield -= damage;
        else CurHp -= damage;

        if (shield < 0)
            curHp += shield;
    }
    public void Heal(int heal)
    {
        curHp += heal;
        if(curHp > maxHp)
            curHp = maxHp;
    }
}
