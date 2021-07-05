using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    int Length;

    [SerializeField]
    GameObject CellPrefab;

    List<GameObject> Body;

    public Vector2Int Direction { get; private set; }

    void Awake()
    {
        Direction = new Vector2Int(0, 1);
        Body = new List<GameObject>();

        for (int i = 0; i < Length; i++)
        {
            var cell = Instantiate(CellPrefab, gameObject.transform, false);
            cell.name = $"{i}";
            cell.transform.localPosition = 100f * i * Vector3.up;
            Body.Add(cell);
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
