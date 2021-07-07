using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField, Min(6)]
    Vector2Int GridSize;

    public Vector2Int cellSize { get; private set; }
    RectTransform rectTransform;

    public GameObject[,] Grid { get; private set; }

    void Awake()
    {
        Grid = new GameObject[GridSize.x, GridSize.y];
        rectTransform = GetComponent<RectTransform>();
        cellSize = new Vector2Int(
            Mathf.FloorToInt(rectTransform.rect.width / GridSize.x),
            Mathf.FloorToInt(rectTransform.rect.height / GridSize.y));

        Clock.OnTick += UpdateGrid;
    }

    void UpdateGrid()
    {
        var children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in children)
        {
            if (!child.CompareTag("GridObject") && !child.CompareTag("SnakeCell"))
                continue;

            Vector2Int cell = LocalToGrid(child.anchoredPosition);
            if (cell.x > GridSize.x || cell.y > GridSize.y)
                Debug.LogError("GameOver not implemented yet.");
            else
                Grid[cell.x, cell.y] = child.gameObject;
            //Debug.Log($"{child.anchoredPosition}: {cell.x} {cell.y}");
        }
    }

    int CellX(float x) => Mathf.RoundToInt(x / cellSize.x);
    int CellY(float y) => Mathf.RoundToInt(y / cellSize.y); 

    Vector2Int LocalToGrid(Vector2 localPos) => new Vector2Int(CellX(localPos.x), CellY(localPos.y));
}
