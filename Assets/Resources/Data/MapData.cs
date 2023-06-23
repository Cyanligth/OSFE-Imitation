using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Data/Map")]
public class MapData : ScriptableObject
{
    public enum World { Fire, Forest, Ice, Ruins, Wasteland, Count, Eden = 10 }
    public enum Room { Battle, Distress, Hazard, Camp, Shop, Treasure, Miniboss, Count, Boss, Next }

    public List<Maps> maps = new List<Maps>();
    [Serializable]
    public class Maps
    {
        public World world;
        public Sprite worldIcon;
    }
    public List<Rooms> rooms = new List<Rooms>();
    [Serializable]
    public class Rooms
    {
        public Room room;
        public Sprite roomIcon;
    }

}
