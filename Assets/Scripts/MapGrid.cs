using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField]
    Vector2Int Size;

    GameObject[] Grid;

    void Awake()
    {
        Grid = new GameObject[Size.x * Size.y];
    }
}
