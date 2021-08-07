using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField, Min(0.1f)]
    float clockSpeed = 0.5f;
    public float ClockSpeed => clockSpeed;

    [SerializeField, Min(0f)]
    float SpeedIncreaseStep = 0.03f;

    float tempTime;

    public static Action OnPreTick, OnTick;

    static bool Pause;
    public static void PauseClock() => Pause = true;
    public static void ResumeClock() => Pause = false;

    void Awake()
    {
        Pause = false;
        OnPreTick = null;
        OnTick = null;
    }

    void Start()
    {
        MapGrid.OnFoodTaken += IncreaseClockSpeed;
    }

    void Update()
    {
        if (Pause)
            return;

        tempTime += Time.deltaTime;
        if (tempTime >= ClockSpeed)
        {
            tempTime = 0f;
            OnPreTick?.Invoke();
            if (!Pause)
                OnTick?.Invoke();
        }
    }

    void IncreaseClockSpeed()
    {
        clockSpeed = Mathf.Max(0.1f, clockSpeed - SpeedIncreaseStep);
    }

    void OnDestroy()
    {
        MapGrid.OnFoodTaken -= IncreaseClockSpeed;
    }
}
