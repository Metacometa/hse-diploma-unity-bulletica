using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTestPlayMode
{
    [UnityTest]
    public IEnumerator TestWithCoroutine()
    {
        yield return null;
        Assert.IsTrue(true);
    }
}