using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    int Length;

    [SerializeField]
    GameObject CellPrefab;

    LinkedList<GameObject> Body;

    public Vector2Int Direction { get; private set; }

    void Awake()
    {
        Direction = new Vector2Int(0, 1);
        Body = new LinkedList<GameObject>();
        for (int i = 0; i < Length; i++)
        {
            var cell = Instantiate(CellPrefab, gameObject.transform);
            cell.name = $"{i}";
            cell.transform.position = Vector3.up * i * 100f;
            Body.AddFirst(cell);
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
        var head = Body.First;
        var prevPos = head.Value.transform.position;
        head.Value.transform.position += (new Vector3(Direction.x, Direction.y, 0) * 50f);
        var next = head.Next;
        while (next != null)
        {
            var tempPos = next.Value.transform.position;
            next.Value.transform.position = prevPos;
            prevPos = tempPos;
            next = next.Next;
        }
    }
}
