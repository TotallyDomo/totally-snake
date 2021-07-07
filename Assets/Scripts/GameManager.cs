using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action<bool> OnGameEnd;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }
}
