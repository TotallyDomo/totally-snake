using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Vector2Int Direction { get; private set; }

    [SerializeField]
    GameObject SnakeCellPrefab;

    List<RectTransform> Body;
    Vector2Int lastDirection, headCell;
    Vector2 prevPos;

    void Awake()
    {
        Direction = new Vector2Int(0, 1);
        Body = new List<RectTransform>();

        // Fancy way of iterating over all child-objects
        foreach (RectTransform child in transform)
        {
            if (child.CompareTag("SnakeCell"))
                Body.Add(child);
        }
        headCell = MapGrid.Instance.LocalToGrid(Body[0].anchoredPosition);
    }

    void Start()
    {
        Clock.OnPreTick += MoveBody;
        MapGrid.OnFoodTaken += SpawnSnakeCell;
    }

    public void UpdateDirection(Vector2Int dir)
    {
        if (lastDirection.x + dir.x != 0 && lastDirection.y + dir.y != 0)
            Direction = dir;
    }

    void MoveBody()
    {
        prevPos = Body[0].anchoredPosition;
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
        lastDirection = Direction;
        headCell += Direction;
        Body[0].anchoredPosition = MapGrid.Instance.GridToLocal(headCell);
    }

    public void SpawnSnakeCell()
    {
        var go = Instantiate(SnakeCellPrefab, MapGrid.Instance.transform);
        var rect = go.GetComponent<RectTransform>();
        rect.anchoredPosition = prevPos;
        Body.Add(rect);
    }
}
