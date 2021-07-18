using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject FoodCellPrefab;

    void Start()
    {
        MapGrid.OnFoodTaken += SpawnFoodCell;
        SpawnFoodCell();
    }

    public void SpawnFoodCell()
    {
        var go = Instantiate(FoodCellPrefab, MapGrid.Instance.transform);
        var rect = go.GetComponent<RectTransform>();
        var cells = FindPossibleCells();
        int randInt = Random.Range(0, cells.Count);
        rect.anchoredPosition = MapGrid.Instance.GridToLocal(cells[randInt]);
    }

    List<Vector2Int> FindPossibleCells()
    {
        var output = new List<Vector2Int>();
        for (int x = 0; x < MapGrid.Instance.GridSize.x; x++)
        {
            for (int y = 0; y < MapGrid.Instance.GridSize.y; y++)
            {
                if (MapGrid.Instance.Grid[x, y] == null)
                    output.Add(new Vector2Int(x, y));
            }
        }
        return output;
    }
}
