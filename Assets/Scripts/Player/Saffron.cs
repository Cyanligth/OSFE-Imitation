using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Saffron : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Player.Job = "Saffron";
        GameManager.Player.MaxHp = 1200;
        GameManager.Player.CurHp = GameManager.Player.MaxHp;
        GameManager.Player.Level = 1;
        GameManager.Player.ManaRegen = 0.5f;
        GameManager.Player.Money = 0;
        GameManager.Player.MaxMana = 3;
        GameManager.Player.CurMana = 0;
        GameManager.Player.CurExp = 0;
        GameManager.Player.MaxExp = 50;
        GameManager.Player.Hand[0] = null;
        GameManager.Player.Hand[1] = null;

        GameManager.Player.GetNewCard(GameManager.Resource.Load<CardData>("Data/Card/Thunder"));
        GameManager.Player.GetNewCard(GameManager.Resource.Load<CardData>("Data/Card/KineticWave"));
        GameManager.Player.GetNewCard(GameManager.Resource.Load<CardData>("Data/Card/FrostBolt"));
        GameManager.Player.GetNewCard(GameManager.Resource.Load<CardData>("Data/Card/StepSlash"));
        GameManager.Player.GetNewCard(GameManager.Resource.Load<CardData>("Data/Card/SwordLine"));
    }
}
