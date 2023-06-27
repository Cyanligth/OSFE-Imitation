using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardData;

[CreateAssetMenu(fileName = "CardList", menuName = "Data/CardList")]
public class CardList : ScriptableObject
{
    [SerializeField] List<CardData> cards;

    public List<CardData> Cards { get { return cards; } }

    // public void UseCard(CardData card, Transform transform)
    // {
    //     card.effectPos = transform;
    //     Card ccc = GameManager.Resource.Instantiate<Card>($"Effect/{card.spell}", card.effectPos.position, card.effectPos.rotation);
    //     GameManager.Resource.Destroy(ccc);
    // }
}
