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

    // 시드를 외부파일에 저장 == 절대 잊으면 안됨
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
        // 아래에서 찾아서 할것들 매니저에도 만들고 해놓고
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

    // 예정경로 바뀔때마다 원래색으로 돌리는것도 실행해주기 
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
    // 지금이 몇번째 월드인지 체크
    // 맵UI프리팹 가져온 후 맵의 제일 위 혹은 아래에서 한칸 띈 사이 어딘가의 자식으로 넣는다.
    // 월드 랜덤 돌리기
    // 월드네임 랜덤? -> 비행기에서 쓰는 그 알파벳 브라보 뭐시기인 그걸로 대충 해놓자
    // 나온 월드 배경으로 바꿔주고 아래의 마름모에 월드 아이콘 넣어주고 현재 월드로 표기
    // 맵 제작 시작
    /*
        각 노드는 자신이 무엇인지, 다음으로 갈 수 있는 노드가 무엇이 있는지
        그중에서 연결된게 무엇인지를 기억한다.
        각각 최소로 연결되는 경우와 최대로 연결되는 경우를 지닌다.
        
    */
    // 첫번째는 무조건 전투
    // 두번째는 무조건 3개
    // 무조건 상점, 보물, 캠프, 미니보스가 하나씩은 있어야함
    // 7번째는 보스
    // 8번째는 무조건 3개, 월드 랜덤으로 돌려서 넣는다. 중복 허용
    // 끝까지 갔으면 지우고 새로 만든다
}