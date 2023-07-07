using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomUI : BaseUI, ISpellEventListener
{
    EventMaster master;
    Queue<DeckStackIcon> deckStackIcons = new Queue<DeckStackIcon>(); 
    protected override void Awake()
    {
        base.Awake();
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
    }
    private void OnEnable()
    {
        master.AddSpellEventListener(this);
    }
    private void OnDisable()
    {
        master.RemoveSpellEventListener(this);
    }

    public void DeleteCardEvent(CardData card)
    {
    }

    public void GetNewCardEvent(CardData card)
    {
    }

    public void Hand1UseEvent()
    {
        GameManager.Resource.Instantiate<GameObject>("Effect/SlotFrameEject", images["Hand1Pos"].rectTransform.rect.position, Quaternion.identity);
        if (GameManager.Player.Hand[0] != null)
        {
            images["Hand1Pos"].sprite = GameManager.Player.Hand[0].icon;
        }
        else
        {
            images["Hand1Pos"].sprite = null;
        }
        if(deckStackIcons.Count > 0)
        {
            GameManager.Resource.Destroy(deckStackIcons.Dequeue().gameObject);
        }
        texts["DeckCountText"].text = $"{GameManager.Player.Deck.Count}/{GameManager.Player.CardList.Count}";
    }

    public void Hand2UseEvent()
    {
        GameManager.Resource.Instantiate<GameObject>("Effect/SlotFrameEject", images["Hand2Pos"].rectTransform.rect.position, Quaternion.identity);
        if (GameManager.Player.Hand[1] != null)
        {
            images["Hand2Pos"].sprite = GameManager.Player.Hand[1].icon;
        }
        else
        {
            images["Hand2Pos"].sprite = null;
        }
        if (deckStackIcons.Count > 0)
        {
            GameManager.Resource.Destroy(deckStackIcons.Dequeue().gameObject);
        }
        texts["DeckCountText"].text = $"{GameManager.Player.Deck.Count}/{GameManager.Player.CardList.Count}";
    }

    public void ShuffleEvent()
    {
        images["Hand1Pos"].sprite = GameManager.Player.Hand[0].icon;
        images["Hand2Pos"].sprite = GameManager.Player.Hand[1].icon;
        while(deckStackIcons.Count > 0)
        {
            GameManager.Resource.Destroy(deckStackIcons.Dequeue().gameObject);
        }
        foreach(CardData card in GameManager.Player.Deck)
        {
            DeckStackIcon icon = GameManager.Resource.Instantiate<DeckStackIcon>("SpellIcon/SpellIconFrame", images["DeckStack"].rectTransform.transform);
            icon.IconSet(card.icon);
            deckStackIcons.Enqueue(icon);
        }
        texts["DeckCountText"].text = $"{GameManager.Player.Deck.Count}/{GameManager.Player.CardList.Count}";
    }


    
}
