using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class ClockTests
{
    Clock clock;

    [SetUp]
    public void SetUp()
    {
        clock = new GameObject().AddComponent<Clock>();
    }

    [TearDown]
    public void TearDown()
    {
        Clock.OnTick = null;
        Clock.OnPreTick = null;
        Object.Destroy(clock.gameObject);
    }

    [UnityTest]
    public IEnumerator OnTick_Works()
    {
        int count = 0;
        Clock.OnTick += () => count++;
        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(1, count);

        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator OnPreTick_Works()
    {
        int count = 0;
        Clock.OnPreTick += () => count++;
        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(1, count);

        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(2, count);
    }

    [UnityTest]
    public IEnumerator OnPreTick_IsExecutedBeforeOnTick()
    {
        int count = 0;
        Clock.OnTick += () => count++;
        Clock.OnPreTick += () => Assert.AreEqual(0, count);

        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator PauseAndResume_Works()
    {
        int count = 0;
        Clock.OnTick += () => count++;
        Clock.PauseClock();

        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(0, count);

        Clock.ResumeClock();

        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(1, count);
    }

    [UnityTest]
    public IEnumerator PauseOnPreTick_StopsOnTick()
    {
        int count = 0;
        Clock.OnPreTick += () => Clock.PauseClock();
        Clock.OnTick += () => count++;

        yield return new WaitForSeconds(clock.ClockSpeed);
        Assert.AreEqual(0, count);
    }

    [UnityTest]
    public IEnumerator PickingFood_SpeedsUpClockTime()
    {
        var clockSpeed = clock.ClockSpeed;
        MapGrid.OnFoodTaken.Invoke();
        yield return null;
        Assert.IsTrue(clock.ClockSpeed < clockSpeed);
    }

    [UnityTest]
    public IEnumerator ClockSpeed_StaysAboveSpecifiedLimit()
    {
        for (int i = 0; i < 10000; i++)
            MapGrid.OnFoodTaken.Invoke();
        yield return null;
        Assert.IsTrue(clock.ClockSpeed >= clock.MinimalClockSpeed);
    }
}