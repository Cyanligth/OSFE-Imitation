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

    private void Awake()
    {
        BindChildren();
        tileData = GameManager.Resource.Load<TileData>("Data/TileData");
    }

    public void SetTile(int tile)
    {
        sprites["State"].sprite = tileData.tileImg[tile];
    }
    public void SetBorder(int border)
    {
        sprites["Border"].sprite = tileData.borderImg[border];
    }
    public void SetTileRoutine(int tile, float time)
    {
        StartCoroutine(TileChanging(tile, time));
    }
    public void SetTileRoutine(int tile, float time, int endTile)
    {
        StartCoroutine(TileChanging(tile, time, endTile));
    }
    IEnumerator TileChanging(int tile, float time)
    {
        SetTile(tile);
        yield return new WaitForSeconds(time);
        SetTile(0);
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
