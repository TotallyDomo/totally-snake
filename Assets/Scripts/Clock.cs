using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField, Min(0.1f)]
    float clockSpeed = 0.5f;
    public float ClockSpeed => clockSpeed;

    [SerializeField, Min(0f)]
    float speedIncreaseStep = 0.03f;

    [SerializeField, Min(0.08f)]
    float minimalClockSpeed = 0.08f;
    public float MinimalClockSpeed => minimalClockSpeed;

    float tempTime;

    public static Action OnPreTick, OnTick;

    static bool pause;
    public static void PauseClock() => pause = true;
    public static void ResumeClock() => pause = false;

    void Awake()
    {
        pause = false;
        OnPreTick = null;
        OnTick = null;
    }

    void Start()
    {
        MapGrid.OnFoodTaken += IncreaseClockSpeed;
    }

    void Update()
    {
        if (pause)
            return;

        tempTime += Time.deltaTime;
        if (tempTime >= ClockSpeed)
        {
            tempTime = 0f;
            OnPreTick?.Invoke();
            if (!pause)
                OnTick?.Invoke();
        }
    }

    void IncreaseClockSpeed()
    {
        clockSpeed = Mathf.Max(minimalClockSpeed, clockSpeed - speedIncreaseStep);
    }

    void OnDestroy()
    {
        MapGrid.OnFoodTaken -= IncreaseClockSpeed;
    }
}
