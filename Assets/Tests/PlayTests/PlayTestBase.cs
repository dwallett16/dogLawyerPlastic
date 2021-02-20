
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayTestBase: TestDataFactory
{
    private List<GameObject> gameObjects;

    public PlayTestBase() {
        gameObjects = new List<GameObject>();
    }

    protected void AddToCleanup(GameObject gameObject) {
        gameObjects.Add(gameObject);
    }

    [TearDown]
    protected void Teardown()
    {
        foreach(var obj in gameObjects) {
            Object.Destroy(obj);
        }
        gameObjects.Clear();
    }
}
