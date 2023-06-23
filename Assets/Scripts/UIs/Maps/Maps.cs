using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : BaseUI
{
    RandomMapGenerator mapGenerator;
    public enum World { Fire, Forest, Ice, Ruins, Wasteland, Count, Eden = 10 }
    public enum Room { Battle, Distress, Hazard, Camp, Shop, Treasure, Miniboss, Count, Boss, Next }
    RectTransform rect;
    private Vector2[] pivotPreset;
    int anchorPosNum = 0;

    protected MapData mapData;
    protected override void Awake()
    {
        base.Awake();
        mapData = GameManager.Resource.Load<MapData>("Data/MapData");
        mapGenerator = GetComponent<RandomMapGenerator>();
        mapGenerator.OnMapGenerated.AddListener(() => { MoveAnchor(2); });
        rect = GetComponent<RectTransform>();
        pivotPreset = new Vector2[10];
        for(int i = 0; i < 10; i++)
        {
            pivotPreset[i] = new Vector2(1f / 10f * i, 0.5f);
        }
    }
    
    public void NextWorld()
    {
        mapGenerator.InitMap();
    }
    // 맵의 앵커를 현재 맵 아이콘의 위치로 설정(X만, Y는 높이유지 위해 그대로)
    // 스테이지 클리어시마다 호출
    public void MoveAnchor(int i)
    {
        rect.pivot = pivotPreset[i];
        rect.anchoredPosition = new Vector2(0, 0);
    }
}
