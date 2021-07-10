using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGrid))]
public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject SnakeCellPrefab, FoodCellPrefab;

    MapGrid mapGrid;

    public void SpawnSnakeCell(RectTransform cell)
    {
        //if (!CheckInput(cell))
        //    return;
        //var spawnCell = 
        //Instantiate(SnakeCellPrefab,)
    }

    public void SpawnFoodCell(Vector2Int cell)
    {

    }

    bool CheckInput(Vector2Int cell)
    {
        if (cell.x >= mapGrid.GridSize.x || cell.y >= mapGrid.GridSize.y)
        {
            Debug.LogError("Attempting to spawn an GridObject outside grid bounds.");
            Clock.PauseClock();
            return false;
        }
        return true;
    }
}
