using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    GameObject CellPrefab;

    List<GameObject> Body;

    public Vector2Int Direction { get; private set; }

    void Awake()
    {
        Direction = new Vector2Int(0, 1);
        Body = new List<GameObject>();

        // Fancy way of iterating over all child-objects
        foreach (Transform child in transform)
        {
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
        Body[0].transform.position += new Vector3(Direction.x, Direction.y, 0) * 50f;
        for (int i = 1; i < Body.Count; i++)
        {
            var tempPos = Body[i].transform.position;
            Body[i].transform.position = prevPos;
            prevPos = tempPos;
        }
    }
}
