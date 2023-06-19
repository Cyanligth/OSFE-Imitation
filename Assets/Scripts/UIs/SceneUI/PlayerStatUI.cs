using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatUI : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
        texts["ManaRegenText"].text = GameManager.Player.ManaRegen.ToString() + " Mana";
        // texts["AtkDmgText"].text
        // texts["SpellDmgText"].text
        // texts["ShiledText"].text
        // texts["LuckText"].text
    }
}
