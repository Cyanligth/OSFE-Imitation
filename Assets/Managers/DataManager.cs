using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int seed;

    public enum Property { None, Anima, Convergence, Doublelift, Glimmer, Hearth, Hexawan, Kinesys, Miseri, Phalanx, Slashfik }
    public enum World { Fire, Forest, Ice, Ruins, Wasteland, Count, Eden = 10 }
    public enum Tile { Normal, Used, Crecked, Broken, Count }
    public enum TileBorder { Blue, Red, Netural, Count }
    public Tiles[,] tileList = new Tiles[8,4];
    public bool[,] tileMap = new bool[8,4];
    public Vector2[,] map = new Vector2[8,4];
    public int playerXPos;
    public int playerYPos;
    public Property curFocus1;
    public Property curFocus2;
    public World curWorld;

    private void Awake()
    {
        CreateMap();
        curFocus1 = Property.None;
        curFocus2 = Property.None;
        playerXPos = 1; 
        playerYPos = 2;
    }

    public Property CurFocus1 { get { return curFocus1; } set { curFocus1 = value; } }
    public Property CurFocus2 { get { return curFocus2; } set { curFocus2 = value; } }

    public void CreateMap()
    {
        float o = -5.55f; //-4.05
        float p = 0.4f;
        for(int i = 0; i < map.GetLength(0); i++)
        {
            p = 0.4f;
            for(int j = 0; j < map.GetLength(1); j++)
            {
                TileBorder border;
                if (i >= 4)
                {
                    tileMap[i, j] = false;
                    border = TileBorder.Red;
                }
                else
                {
                    tileMap[i, j] = true;
                    border = TileBorder.Blue;
                }
                    
                map[i, j] = new Vector2(o,p);
                tileList[i, j] = SetTile(o, p, border, Tile.Normal);
                p -= 1f;
            }
            o += 1.6f;
        }
    }
    // -3 -> ++2, 2 -> --1.25
    public Tiles SetTile(float x, float y, TileBorder border, Tile tile)
    {
        Tiles tiles = GameManager.Resource.Instantiate<Tiles>("Prefabs/Tile", new Vector3(x, y), Quaternion.identity, transform);
        tiles.SetTile((int)tile);
        tiles.SetBorder((int)border);
        return tiles;
    }
       
}
