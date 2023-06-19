using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public bool[,] tileMap = new bool[8,4];
    public Vector2[,] map = new Vector2[8,4];

    private void Awake()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        float o = -5.55f; //-4.05
        float p = 0.4f;
        for(int i = 0; i < map.GetLength(0); i++)
        {
            p = 0.4f;
            for(int j = 0; j < map.GetLength(1); j++)
            {
                if (i >= 4)
                    tileMap[i, j] = false;
                else
                    tileMap[i,j] = true;
                map[i, j] = new Vector2(o,p);
                p -= 1f;
            }
            o += 1.6f;
        }
    }
    // -3 -> ++2, 2 -> --1.25
    public class EnemyDataContainer
    {

    }
    public class EnemyData
    {

    }
}
