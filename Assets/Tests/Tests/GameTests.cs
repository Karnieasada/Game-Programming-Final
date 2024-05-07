using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTests
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Game");
    }

    [UnityTest]
    public IEnumerator Player_Name_Shown()
    {
        GameObject name = GameObject.Find("NameText");
        string player = "Jeff";
        name.GetComponent<Text>().text = player;
        Assert.NotNull(name.GetComponent<Text>());
        yield return new WaitForSeconds(.3f);
    }

    [UnityTest]
    public IEnumerator All_Targets_End_Game()
    {
        GameObject player = GameObject.Find("Player");
        for (int i = 0; i < 5; i++)
        {
            GameObject target = GameObject.Find("Reward(Clone)");
            player.transform.position = target.transform.position;
            yield return new WaitForSeconds(.3f);
        }

        string name = SceneManager.GetActiveScene().name;
        string expected = "Exit";
        Assert.AreEqual(expected, name);
    }

    [UnityTest]
    public IEnumerator Stop_Game()
    {
        GameObject button = GameObject.Find("StopButton");
        button.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1f);

        string name = SceneManager.GetActiveScene().name;
        string expected = "Exit";
        Assert.AreEqual(expected, name);
        yield return new WaitForSeconds(.3f);
    }

    [TearDown]
    public void Teardown()
    {
        SceneManager.LoadScene("Game");
    }
}
