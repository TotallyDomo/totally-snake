using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGrid))]
public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject SnakeCellPrefab, FoodCellPrefab;

    MapGrid mapGrid;

    void Awake()
    {
        mapGrid = GetComponent<MapGrid>();
    }

    public RectTransform SpawnSnakeCell(RectTransform cell)
    {
        if (!ValidateInput(MapGrid.LocalToGrid(cell.anchoredPosition)))
            return null;

        var go = Instantiate(SnakeCellPrefab, mapGrid.transform);
        var rect = go.GetComponent<RectTransform>();
        rect.anchoredPosition = cell.anchoredPosition;
        return rect;
    }

    public RectTransform SpawnFoodCell()
    {
        var go = Instantiate(FoodCellPrefab, mapGrid.transform);
        var rect = go.GetComponent<RectTransform>();
        var cells = FindPossibleCells();
        int randInt = Random.Range(0, cells.Count);
        rect.anchoredPosition = MapGrid.GridToLocal(cells[randInt]);
        return rect;
    }

    bool ValidateInput(Vector2Int cell)
    {
        if (cell.x >= mapGrid.GridSize.x || cell.y >= mapGrid.GridSize.y)
        {
            Debug.LogError("Attempting to spawn an GridObject outside grid bounds.");
            Clock.PauseClock();
            return false;
        }
        return true;
    }

    List<Vector2Int> FindPossibleCells()
    {
        var output = new List<Vector2Int>();
        for (int x = 0; x < mapGrid.GridSize.x; x++)
        {
            for (int y = 0; y < mapGrid.GridSize.y; y++)
            {
                if (mapGrid.Grid[x, y] == null)
                    output.Add(new Vector2Int(x, y));
            }
        }
        return output;
    }
}
