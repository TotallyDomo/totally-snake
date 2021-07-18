using System;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    public static MapGrid Instance;

    [SerializeField, Min(6)]
    Vector2Int gridSize;

    public Vector2Int GridSize => gridSize;
    public GameObject[,] Grid { get; private set; }
    public Vector2Int cellSize { get; private set; }
    RectTransform rectTransform;

    public static Action OnFoodTaken;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        OnFoodTaken = null;

        Grid = new GameObject[GridSize.x, GridSize.y];
        rectTransform = GetComponent<RectTransform>();
        cellSize = new Vector2Int(
            Mathf.FloorToInt(rectTransform.rect.width / GridSize.x),
            Mathf.FloorToInt(rectTransform.rect.height / GridSize.y));

        UpdateGrid();
    }

    void Start()
    {
        Clock.OnTick += UpdateGrid;
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
            if (CheckCell(cell, child))
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

    bool CheckCell(Vector2Int cell, RectTransform child)
    {
        if (cell.x >= GridSize.x || cell.x < 0 ||
            cell.y >= GridSize.y || cell.y < 0)
        {
            GameManager.Instance.TriggerGameOver();
            return false;
        }
        else if (Grid[cell.x, cell.y] == null)
            return true;

        var obj = Grid[cell.x, cell.y];
        if (obj.CompareTag("SnakeCell") && child.CompareTag("SnakeCell"))
        {
            GameManager.Instance.TriggerGameOver();
            return false;
        }
        else if (obj.CompareTag("FoodCell") && child.CompareTag("SnakeCell"))
        {
            Destroy(obj);
            Grid[cell.x, cell.y] = null;
            OnFoodTaken?.Invoke();
        }
        else if (obj.CompareTag("SnakeCell") && child.CompareTag("FoodCell"))
        {
            Destroy(child.gameObject);
            OnFoodTaken?.Invoke();
            return false;
        }
        return true;
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
