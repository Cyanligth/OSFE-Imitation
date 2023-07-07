using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    TileData tileData;
    protected Dictionary<string, Transform> transforms;
    protected Dictionary<string, SpriteRenderer> sprites;
    int curTile;
    bool isChanging;

    private void Awake()
    {
        BindChildren();
        tileData = GameManager.Resource.Load<TileData>("Data/TileData");
        curTile = 0;
    }

    public void SetTile(int tile)
    {
        sprites["State"].sprite = tileData.tileImg[tile];
        curTile = tile;
    }
    public void SetBorder(int border)
    {
        sprites["Border"].sprite = tileData.borderImg[border];
    }
    public void SetTileRoutine(int tile, float time)
    {
        if (curTile == 3 && tile == 1)
            return;
        StartCoroutine(TileChanging(tile, time));
    }
    public void SetTileRoutine(int tile, float time, int endTile)
    {   if (isChanging) return;
        StartCoroutine(TileChanging(tile, time, endTile));
    }
    public IEnumerator TileChanging(int tile, float time)
    {
        isChanging = true;
        TileData.Tile pastTile = (TileData.Tile)curTile;
        SetTile(tile);
        yield return new WaitForSeconds(time);
        SetTile((int)pastTile);
        isChanging = false;
        if(curTile == 1)
        {
            SetTile(0);
            curTile = 0;
        }
    }
    IEnumerator TileChanging(int tile, float time, int endTile)
    {
        SetTile(tile);
        yield return new WaitForSeconds(time);
        SetTile(endTile);
    }
    private void BindChildren()
    {
        transforms = new Dictionary<string, Transform>();
        sprites = new Dictionary<string, SpriteRenderer>();

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            string key = child.gameObject.name;
            if (transforms.ContainsKey(key))
                continue;
            transforms.Add(key, child);

            SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
            if (sprite != null)
                sprites.Add(key, sprite);
        }
    }
}
