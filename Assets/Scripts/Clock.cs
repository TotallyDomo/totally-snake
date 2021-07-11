using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField, Min(0.1f)]
    float ClockSpeed = 0.7f;
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

    void Update()
    {
        if (Pause)
            return;

        tempTime += Time.smoothDeltaTime;
        if (tempTime >= ClockSpeed)
        {
            tempTime = 0f;
            OnPreTick?.Invoke();
            if (!Pause)
                OnTick?.Invoke();
        }
    }
}
