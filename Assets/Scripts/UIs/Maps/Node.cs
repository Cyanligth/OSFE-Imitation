using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public Image image;
    public LineRenderer lineRenderer;
    Vector3[] linePoints;
    public RectTransform rect;
    public UnityEvent<Node> MouseOn;
    public UnityEvent<Node> OnClick;

    private void Awake()
    {
        image = GetComponent<Image>();
        lineRenderer = GetComponent<LineRenderer>();
        rect = GetComponent<RectTransform>();
    }
    public void DrawLine()
    {
        linePoints = new Vector3[nextRoom.Count*2];
        lineRenderer.positionCount = linePoints.Length;
        int count = 0;
        foreach (Node node in nextRoom)
        {
            if (node == null)
                break;
            linePoints[count++] = (rect.position);
            linePoints[count++] = (node.rect.position);
        }
        lineRenderer.SetPositions(linePoints);
    }
    public void ClearLine()
    {
        lineRenderer.positionCount=0;
    }

    // 방 하나의 정보
    public int curStage;
    public int room;
    public List<Node> nextRoom;
    public List<Node> prevRoom;
    public List<int> weights;
    public Node(int stage, int room)
    {
        this.curStage = stage;
        this.room = room;
    }
    public void NextLink(Node node, Node nextNode)
    {
        node.nextRoom.Add(nextNode);
    }
    public void PrevLink(Node node, Node prevLink)
    {
        node.prevRoom.Add(prevLink);
    }
    public void WeightLink(Node node, Node nextNode, int weight)
    {
        node.nextRoom.Add(nextNode);
        node.weights.Add(weight);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOn?.Invoke(this);
    }
}
