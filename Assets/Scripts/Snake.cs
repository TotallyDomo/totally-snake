using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGrid), typeof(Spawner))]
public class Snake : MonoBehaviour
{
    public Vector2Int Direction { get; private set; }
    Vector2Int lastDirection;

    List<RectTransform> Body;
    MapGrid mapGrid;
    Spawner spawner;

    void Awake()
    {
        Direction = new Vector2Int(0, 1);
        Body = new List<RectTransform>();
        mapGrid = GetComponent<MapGrid>();
        spawner = GetComponent<Spawner>();

        // Fancy way of iterating over all child-objects
        foreach (RectTransform child in transform)
        {
            if (child.CompareTag("SnakeCell"))
                Body.Add(child);
        }

        Clock.OnPreTick += MoveBody;
    }  

    public void UpdateDirection(Vector2Int dir)
    {
        if (lastDirection.x + dir.x != 0 && lastDirection.y + dir.y != 0)
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
        lastDirection = Direction;
        var newHeadCell = MapGrid.LocalToGrid(Body[0].anchoredPosition) + Direction;
        CheckNextCell(newHeadCell);
        Body[0].anchoredPosition = MapGrid.GridToLocal(newHeadCell);
    }

    void CheckNextCell(Vector2Int cell)
    {
        if (cell.x >= mapGrid.GridSize.x || cell.x < 0 ||
            cell.y >= mapGrid.GridSize.y || cell.y < 0 ||
            (mapGrid.Grid[cell.x, cell.y] != null && mapGrid.Grid[cell.x, cell.y].CompareTag("SnakeCell")))
        {
            GameManager.Instance.TriggerGameOver();
        }
        else if (mapGrid.Grid[cell.x, cell.y] != null && 
            mapGrid.Grid[cell.x, cell.y].CompareTag("FoodCell"))
        {
            var newCell = spawner.SpawnSnakeCell(Body[Body.Count - 1]);
            Body.Add(newCell);
            Destroy(mapGrid.Grid[cell.x, cell.y]);
            mapGrid.Grid[cell.x, cell.y] = null;
            spawner.SpawnFoodCell();
        }  
    }
}
