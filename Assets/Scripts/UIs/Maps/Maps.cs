using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : BaseUI
{
    RandomMapGenerator mapGenerator;
    public enum World { Fire, Forest, Ice, Ruins, Wasteland, Count, Eden = 10 }
    public enum Room { Battle, Distress, Hazard, Camp, Shop, Treasure, Miniboss, Count, Boss, Next }
    public RectTransform rect;
    Map map;

    protected MapData mapData;
    protected override void Awake()
    {
        base.Awake();
        mapData = GameManager.Resource.Load<MapData>("Data/MapData");
        mapGenerator = GetComponent<RandomMapGenerator>();
        map = GetComponentInParent<Map>();
        rect = GetComponent<RectTransform>();
        // map.OnDraw.AddListener(() => { MoveAnchor(); });
    }

    public void NextWorld()
    {
        mapGenerator.InitMap();
    }
    // 맵의 앵커를 현재 맵 아이콘의 위치로 설정(X만, Y는 높이유지 위해 그대로)
    // 스테이지 클리어시마다 호출
    public void MoveAnchor()
    {
        rect.anchoredPosition = new Vector2(450, 0);
    }
}
