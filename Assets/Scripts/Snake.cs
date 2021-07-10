using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGrid))]
public class Snake : MonoBehaviour
{
    public Vector2Int Direction { get; private set; }

    List<RectTransform> Body;
    MapGrid grid;

    void Awake()
    {
        Direction = new Vector2Int(0, 1);
        Body = new List<RectTransform>();
        grid = GetComponent<MapGrid>();

        // Fancy way of iterating over all child-objects
        foreach (RectTransform child in transform)
        {
            if (child.CompareTag("SnakeCell"))
                Body.Add(child);
        }

        Clock.OnTick += MoveBody;
    }  

    public void UpdateDirection(Vector2Int dir)
    {
        if (Direction.x + dir.x != 0 && Direction.y + dir.y != 0)
            Direction = dir;
    }

    void MoveBody()
    {
        var prevPos = Body[0].anchoredPosition;
        MoveHead();

        for (int i = 1; i < Body.Count; i++)
        {
            var tempPos = Body[i].anchoredPosition;
            Body[i].anchoredPosition = prevPos;
            prevPos = tempPos;
        }
    }

    void MoveHead()
    {
        var headCellPos = MapGrid.LocalToGrid(Body[0].anchoredPosition);
        Body[0].anchoredPosition = MapGrid.GridToLocal(headCellPos += Direction);
    }
}
