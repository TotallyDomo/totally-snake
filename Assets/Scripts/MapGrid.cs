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

    float HalfWidth => rectTransform.rect.width / 2f;
    float HalfHeight => rectTransform.rect.height / 2f;

    void Awake()
    {
        Grid = new GameObject[GridSize.x, GridSize.y];
        rectTransform = GetComponent<RectTransform>();
        cellSize = new Vector2Int(
            Mathf.FloorToInt(rectTransform.rect.width / GridSize.x),
            Mathf.FloorToInt(rectTransform.rect.height / GridSize.y));

        OnTransformChildrenChanged();
    }

    void OnTransformChildrenChanged()
    {
        var children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in children)
        {
            if (!child.CompareTag("GridObject") && !child.CompareTag("SnakeCell"))
                continue;

            Vector2Int cell = LocalToGrid(child.anchoredPosition);
            Grid[cell.x, cell.y] = child.gameObject;
            Debug.Log($"{Grid[cell.x, cell.y]}: {cell.x} {cell.y}");
        }
    }

    int CellX(float x) => Mathf.RoundToInt(x / cellSize.x);
    int CellY(float y) => Mathf.RoundToInt(y / cellSize.y); 

    Vector2Int LocalToGrid(Vector2 localPos) => new Vector2Int(CellX(localPos.x), CellY(localPos.y));
}
