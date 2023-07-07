using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatUI : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
        
    }
    private void Start()
    {
        SetStat();
    }
    public void SetStat()
    {
        texts["ManaRegenText"].text = GameManager.Player.ManaRegen.ToString() + " mana/s";
        texts["AtkDmgText"].text = GameManager.Player.Atk.ToString();
        texts["SpellDmgText"].text = GameManager.Player.Matk.ToString();
        texts["ShieldText"].text = GameManager.Player.Shield.ToString();
        texts["LuckText"].text = GameManager.Player.Luck.ToString();
    }
}
