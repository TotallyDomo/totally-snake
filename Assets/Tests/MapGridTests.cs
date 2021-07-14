using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using System;

public class MapGridTests
{

    MapGrid mapGrid;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var path = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("MapGrid")[0]);
        var asset = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        var go = (GameObject)MonoBehaviour.Instantiate(asset);
        mapGrid = go.GetComponent<MapGrid>();
    }

    [Test]
    public void LocalToGrid_Works_WithPositiveCoordinates()
    {
        var gridPos = mapGrid.LocalToGrid(Vector2.zero);
        Assert.AreEqual(gridPos, Vector2Int.zero);

        gridPos = mapGrid.LocalToGrid(Vector2.one);
        Assert.AreEqual(gridPos, Vector2Int.zero);

        var gridRect = mapGrid.GetComponent<RectTransform>();
        gridPos = mapGrid.LocalToGrid(new Vector2(gridRect.rect.width - 1, gridRect.rect.height - 1));
        Assert.AreEqual(gridPos, new Vector2Int(mapGrid.GridSize.x, mapGrid.GridSize.y));
    }

    [Test]
    public void LocalToGrid_ThrowsArgumentException_WithNegativeCoordinates()
    {
        Assert.That(() => mapGrid.LocalToGrid(new Vector2(-100f, -100f)),
            Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void GridToLocal_Works_WithPositiveCoordinates()
    {
        var localPos = mapGrid.GridToLocal(Vector2Int.zero);
        Assert.AreEqual(localPos, Vector2Int.zero);

        localPos = mapGrid.GridToLocal(Vector2Int.one);
        Assert.AreEqual(localPos, new Vector2Int(mapGrid.cellSize.x, mapGrid.cellSize.y));

        var gridRect = mapGrid.GetComponent<RectTransform>();
        localPos = mapGrid.GridToLocal(new Vector2Int(mapGrid.GridSize.x, mapGrid.GridSize.y));
        Assert.AreEqual(localPos, new Vector2Int((int)gridRect.rect.width, (int)gridRect.rect.height));
    }

    [Test]
    public void GridToLocal_ThrowsArgumentException_WithNegativeCoordinates()
    {
        Assert.That(() => mapGrid.GridToLocal(new Vector2Int(-1, -1)),
            Throws.TypeOf<ArgumentException>());
    }
}