using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite {

    [UnityTest]
    public IEnumerator TestTest() {
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(true);
    }
}
