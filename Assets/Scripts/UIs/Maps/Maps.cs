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
    // ���� ��Ŀ�� ���� �� �������� ��ġ�� ����(X��, Y�� �������� ���� �״��)
    // �������� Ŭ����ø��� ȣ��
    public void MoveAnchor()
    {
        rect.anchoredPosition = new Vector2(450, 0);
    }
}
