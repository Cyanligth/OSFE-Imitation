using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Data/Card")]
[Serializable]
public class CardData : ScriptableObject
{
    public enum Rarity { Common, Rare, Epic, Legendary, Calamity }
    public enum Property { Anima, Convergence, Doublelift, Glimmer, Hearth, Hexawan, Kinesys, Miseri, Phalanx, Slashfik }

    public Rarity rarity;
    public Property property;
    public Sprite icon;
    public Sprite border;
    public Sprite propertyBackground;
    public Sprite propertyIcon;
    public string spell;
    public int damage;
    public int useMana;
    public string effectExplain;
    public string toolTip;
    public float castingTime;
    public float afterDelay;
    public GameObject cardEffect;
}
