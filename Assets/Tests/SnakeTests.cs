using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class SnakeTests
{
    Snake snake;

    [SetUp]
    public void SetUp()
    {
        snake = new GameObject().AddComponent<Snake>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(snake.gameObject);
    }

    [Test]
    public void UpdateDirection_Works()
    {
        var newDirection = Vector2Int.right;
        snake.UpdateDirection(newDirection);
        Assert.AreEqual(snake.Direction, newDirection);

        newDirection = Vector2Int.left;
        snake.UpdateDirection(newDirection);
        Assert.AreEqual(snake.Direction, newDirection);
    }

    [Test]
    public void UpdateDirection_PreventsMovingInOppositeDirection()
    {
        // snake starts with "up" direction
        var prevDirection = snake.Direction;
        snake.UpdateDirection(Vector2Int.down);
        Assert.AreEqual(snake.Direction, prevDirection);
    }
}