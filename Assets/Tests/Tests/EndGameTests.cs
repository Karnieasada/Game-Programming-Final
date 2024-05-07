using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class EndGameTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Exit");
    }
    [UnityTest]
    public IEnumerator ReStart_Game()
    {
        GameObject button = GameObject.Find("PlayAgainButton");
        button.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1f);

        string name = SceneManager.GetActiveScene().name;
        string expected = "Intro";
        Assert.AreEqual(expected, name);
        yield return new WaitForSeconds(.3f);
    }

    [TearDown]
    public void Teardown()
    {
        SceneManager.LoadScene("Exit");
    }
}