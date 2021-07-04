using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField, Min(0.1f)]
    float ClockSpeed = 0.7f;
    float tempTime;

    public static Action OnTick;

    void Update()
    {
        tempTime += Time.smoothDeltaTime;
        if (tempTime >= ClockSpeed)
        {
            tempTime = 0f;
            OnTick?.Invoke();
        }
    }
}
