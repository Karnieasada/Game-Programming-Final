using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GameOpenTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Intro");
    }
    [UnityTest]
    public IEnumerator Start_Game()
    {
        GameObject button = GameObject.Find("PlayButton");
        button.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1f);

        string name = SceneManager.GetActiveScene().name;
        string expected = "Game";
        Assert.AreEqual(expected, name);
        yield return new WaitForSeconds(.3f);
    }

    [UnityTest]
    public IEnumerator Test_Name()
    {
        string expected = "John";
        GameObject namer = GameObject.Find("NameInputField");
        namer.GetComponent<InputField>().text = expected;
        GameObject button = GameObject.Find("PlayButton");
        button.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1f);

        GameObject player = GameObject.Find("NameText");
        string playerName = player.GetComponent<Text>().text;
        Assert.AreEqual(expected, playerName);
        yield return new WaitForSeconds(1f);
    }

    [TearDown]
    public void Teardown()
    {
        SceneManager.LoadScene("Intro");
    }
}