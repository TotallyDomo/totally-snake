using System;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class MapGrid : MonoBehaviour
{
    [SerializeField, Min(6)]
    Vector2Int gridSize;

    public Vector2Int GridSize => gridSize;
    public GameObject[,] Grid { get; private set; }
    public Vector2Int cellSize { get; private set; }
    RectTransform rectTransform;
    Spawner spawner;

    void Awake()
    {
        Grid = new GameObject[GridSize.x, GridSize.y];
        rectTransform = GetComponent<RectTransform>();
        cellSize = new Vector2Int(
            Mathf.FloorToInt(rectTransform.rect.width / GridSize.x),
            Mathf.FloorToInt(rectTransform.rect.height / GridSize.y));

        UpdateGrid();
        spawner = GetComponent<Spawner>();

        Clock.OnTick += UpdateGrid;

    }

    void Start()
    {
        var foodRect = spawner.SpawnFoodCell();
        var foodCell = LocalToGrid(foodRect.anchoredPosition);
        Grid[foodCell.x, foodCell.y] = foodRect.gameObject;
    }

    void UpdateGrid()
    {
        ResetGrid();
        var children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in children)
        {
            if (!child.CompareTag("FoodCell") && !child.CompareTag("SnakeCell"))
                continue;

            Vector2Int cell = LocalToGrid(child.anchoredPosition);
            Grid[cell.x, cell.y] = child.gameObject;
        }
    }

    void ResetGrid()
    {
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Grid[x, y] = null;
            }
        }
    }

    int CellX(float x) => Mathf.RoundToInt(x / cellSize.x);
    int CellY(float y) => Mathf.RoundToInt(y / cellSize.y);

    public Vector2Int LocalToGrid(Vector2 localPos)
    {
        if (localPos.x < 0f || localPos.y < 0f)
            throw new ArgumentException("Negative world positions are not supported.");

        return new Vector2Int(CellX(localPos.x), CellY(localPos.y));
    }
    public Vector2Int GridToLocal(Vector2Int cellPos)
    {
        if (cellPos.x < 0 || cellPos.y < 0)
            throw new ArgumentException("Cell position can't be negative.");

        return new Vector2Int(cellPos.x * cellSize.x, cellPos.y * cellSize.y);
    }
}
