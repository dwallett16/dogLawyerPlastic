
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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
