using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGrid))]
public class Snake : MonoBehaviour
{
    public Vector2Int Direction { get; private set; }

    List<GameObject> Body;
    MapGrid grid;

    void Awake()
    {
        Direction = new Vector2Int(0, 1);
        Body = new List<GameObject>();
        grid = GetComponent<MapGrid>();

        // Fancy way of iterating over all child-objects
        foreach (Transform child in transform)
        {
            if (child.CompareTag("SnakeCell"))
                Body.Add(child.gameObject);
        }

        Clock.OnTick += Move;
    }  

    public void UpdateDirection(Vector2Int dir)
    {
        if (Direction.x + dir.x != 0 && Direction.y + dir.y != 0)
            Direction = dir;
    }

    void Move()
    {
        var prevPos = Body[0].transform.position;
        Body[0].transform.position += new Vector3(
            Direction.x * grid.cellSize.x / 2,
            Direction.y * grid.cellSize.y / 2);

        for (int i = 1; i < Body.Count; i++)
        {
            var tempPos = Body[i].transform.position;
            Body[i].transform.position = prevPos;
            prevPos = tempPos;
        }
    }
}
