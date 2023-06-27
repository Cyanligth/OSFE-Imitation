using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Data/Tile")]
public class TileData : ScriptableObject
{
    public enum Tile { Normal, Used, Crecked, Broken, Count }
    public enum TileBorder { Blue, Red, Netural, Count }
    public TileBorder border;
    public Tile tile;

    public Sprite[] tileImg = new Sprite[(int)Tile.Count];
    public Sprite[] borderImg = new Sprite[(int)TileBorder.Count];

    
}
