using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    protected uint level;
    protected uint maxExp;
    protected uint curExp;
    protected string job;
    protected string weapon;
    protected uint maxHp;
    protected uint curHp;
    protected uint atk;
    protected uint def;
    protected uint shild;
    protected float maxMana;
    protected float curMana;
    protected float manaRegen;
    protected uint money;
    protected Stack<Card> deck;
    protected Queue<Card> grave;
    protected Card[] hand = new Card[2];
    protected List<Card> cardList;
    protected Vector2 playerPos;

    public uint Level { get { return level; } protected set { level = value; } }
    public uint MaxExp { get { return maxExp; } protected set { maxExp = value; } }
    public uint CurExp { get { return curExp; } set { curExp = value; } }
    public string Job { get { return job; } protected set { job = value; } }
    public string Weapon { get { return weapon; } protected set { weapon = value; } }
    public uint MaxHp { get { return maxHp; } protected set { maxHp = value; } }
    public uint CurHp { get { return curHp; } set { curHp = value; } }

    public float MaxMana { get { return maxMana; } protected set { maxMana = value; } }
    public float CurMana { get { return curMana; } set { curMana = value; } }
    public float ManaRegen { get { return manaRegen; } protected set { manaRegen = value; } }
    public uint Money { get { return money; } set { money = value; } }

    public Vector2 PlayerPos { get { return playerPos; } set { playerPos = value; } }
    public Stack<Card> Deck { get { return deck; } set { deck = value; } }
    public Queue<Card> Grave { get { return grave; } set { grave = value; } }
    public Card[] Hand { get { return hand; } set { hand = value; } }
    public List<Card> CardList { get { return cardList; } set { cardList = value; } }

    private void Awake()
    {
        job = "Saffron";
        MaxHp = 1200;
        curHp = maxHp;
        level = 1;
        manaRegen = 0.5f;
        money = 10;
        maxMana = 3;
        curMana = 0;
        curExp = 0;
        maxExp = 50;
        hand[0] = default;
        hand[1] = default;
        Deck = new Stack<Card>();
        Grave = new Queue<Card>();

    }

    public void Draw(int i)
    {
        if(deck.Count > 0)
            hand[i] = deck.Pop();
        else
        {
            if (hand[0] != default && hand[1] != default)
                hand[i] = default;
            else
                Shuffle();
        }
    }
    public void Shuffle()
    {
        StartCoroutine(Shuffling(2f));
        Card[] cards = new Card[grave.Count];
        while(grave.Count > 0)
        {
            int i = Random.Range(0, cards.Length-1);
            if (cards[i] == default)
            {
                cards[i] = grave.Dequeue();
            }
        }
        foreach(Card card in cards)
            deck.Push(card);
        Draw(0);
        Draw(1);
    }
    public void ShuffleHand()
    {
        if (hand[0] != default)
            grave.Enqueue(hand[0]);
        if (hand[1] != default)
            grave.Enqueue(hand[1]);
        foreach(Card card in deck)
            grave.Enqueue(card);
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

    public void ChangeCardList()
    {
        deck.Clear();
        foreach(Card card in cardList)
        {
            grave.Enqueue(card);
        }
        Shuffle();
    }

    public bool isShuffling;
    IEnumerator Shuffling(float i)
    {
        isShuffling = true;
        yield return new WaitForSeconds(i);
        isShuffling = false;
    }
}
