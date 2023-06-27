using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomMapGenerator : Maps
{
    [SerializeField] Transform[] stages; 
    public World nextWorld;
    public UnityEvent OnMapGenerated;
    LineRenderer lineRenderer;
    public List<Node> nodeList;
    public Node curNode;
    public int[] countInt;
    public int seed;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        countInt = new int[8];
        nodeList = new List<Node>();
    }
    private void Start()
    {
        InitMap();
        curNode = nodeList[0];
    }

    // �õ带 �ܺ����Ͽ� ���� == ���� ������ �ȵ�
    public int RandomSeed()
    {
        int rand = 0;
        for(int i = 1; i < 10000001; i *= 10)
        {
            rand += i * Random.Range(0, 10);
        }
        GameManager.Data.seed = rand;
        seed = GameManager.Data.seed;
        return rand;
    }
    
    public void Counting()
    {
        countInt[0] = 1;
        for(int i  = 1; i < 6; i++)
        {
            countInt[i] = Random.Range(2, 4);
        }
        countInt[6] = 1;
        countInt[7] = 3;
    }
    public void Gen()
    {
        Counting();
        for(int i = 0; i < countInt.Length; i++)
        {
            int c = 1;
            for(int j = 0; j < countInt[i]; j++)
            {
                Node node = GameManager.Resource.Instantiate<Node>("UI/Node", stages[i]);
                node.curStage = i;
                node.name = "Node " + node.curStage.ToString() + "-" + c++.ToString();
                switch (i)
                {
                    case 0: node.room = (int)Room.Battle; break;
                    case 6: node.room = (int)Room.Boss; break;
                    case 7: node.room = (int)Room.Next; break;
                    default: node.room = Random.Range(0, (int)Room.Count); break;
                }
                nodeList.Add(node);
            }
        }
    }
    public void RandomLink()
    {
        for(int i = 0; i < nodeList.Count-3; i++)
        {
            for (int j = i + 1; j < i + 4; j++)
            {
                if (nodeList[j].curStage - nodeList[i].curStage != 1)
                    continue;
                if (Random.Range(0, 100) > 49)
                {
                    nodeList[i].NextLink(nodeList[i], nodeList[j]);
                    nodeList[j].PrevLink(nodeList[j], nodeList[i]);
                }
                else
                    continue;
            }
            if (nodeList[i].nextRoom.Count < 1)
            {
                for (int j = i + 1; j < i + 4; j++)
                {
                    if (nodeList[j].curStage - nodeList[i].curStage != 1)
                        continue;
                    nodeList[i].NextLink(nodeList[i], nodeList[j]);
                    nodeList[j].PrevLink(nodeList[j], nodeList[i]);
                    break;
                }
            }
            for (int j = i + 3; j > i; j--)
            {
                if (nodeList[j].curStage - nodeList[i].curStage != 1)
                    continue;
                if (nodeList[j].prevRoom.Count < 1)
                {
                    nodeList[i].NextLink(nodeList[i], nodeList[j]);
                    nodeList[j].PrevLink(nodeList[j], nodeList[i]);
                }
                else
                    continue;
            }
        }
    }
    public void MapGenerating()
    {
        Random.InitState(RandomSeed());
        GameManager.Data.curWorld = (DataManager.World)Random.Range(0, (int)World.Count);
        // �Ʒ����� ã�Ƽ� �Ұ͵� �Ŵ������� ����� �س���
        Counting();
        Gen();
        RandomLink();
        PrintMap();
    }

    public void PrintMap()
    {
        foreach (Node node in nodeList)
        {
            if (node == null)
                continue;
            node.image.sprite = mapData.rooms[node.room].roomIcon;
            node.DrawLine();
        }
        OnMapGenerated?.Invoke();
    }
    public void DrawLine()
    {
        foreach (Node node in nodeList)
        {
            if (node == null)
                continue;
            node.DrawLine();
        }
    }
    public void ClearLine()
    {
        foreach (Node node in nodeList)
        {
            if (node == null)
                continue;
            node.ClearLine();
        }
    }
    public void InitMap()
    {
        foreach(Node node in nodeList)
            GameManager.Resource.Destroy(node.gameObject);
        nodeList.Clear();
        MapGenerating();
    }

    // ������� �ٲ𶧸��� ���������� �����°͵� �������ֱ� 
    public void StartDrawLinkedLine()
    {
        // DrawLinkedLine(curNode, 0);
    }
    public void DrawLinkedLine(Node node, int n)
    {
        if (n >= nodeList.Count)
            return;
        // node.lineRenderer.SetColors(Color.gray, Color.green);
        node.DrawLine();
        foreach(Node linked in node.nextRoom)
        {
            if (linked == null)
                return;
            DrawLinkedLine(linked, n++);
        }
    }
    public void UndrawLinkedLine(Node node, int n)
    {
        if (n >= nodeList.Count)
            return;
        // node.lineRenderer.SetColors(Color.white, Color.white);
        node.DrawLine();
        foreach (Node linked in node.nextRoom)
        {
            if (linked == null)
                return;
            DrawLinkedLine(linked, n++);
        }
    }
    // ������ ���° �������� üũ
    // ��UI������ ������ �� ���� ���� �� Ȥ�� �Ʒ����� ��ĭ �� ���� ����� �ڽ����� �ִ´�.
    // ���� ���� ������
    // ������� ����? -> ����⿡�� ���� �� ���ĺ� ��� ���ñ��� �װɷ� ���� �س���
    // ���� ���� ������� �ٲ��ְ� �Ʒ��� ������ ���� ������ �־��ְ� ���� ����� ǥ��
    // �� ���� ����
    /*
        �� ���� �ڽ��� ��������, �������� �� �� �ִ� ��尡 ������ �ִ���
        ���߿��� ����Ȱ� ���������� ����Ѵ�.
        ���� �ּҷ� ����Ǵ� ���� �ִ�� ����Ǵ� ��츦 ���Ѵ�.
        
    */
    // ù��°�� ������ ����
    // �ι�°�� ������ 3��
    // ������ ����, ����, ķ��, �̴Ϻ����� �ϳ����� �־����
    // 7��°�� ����
    // 8��°�� ������ 3��, ���� �������� ������ �ִ´�. �ߺ� ���
    // ������ ������ ����� ���� �����
}