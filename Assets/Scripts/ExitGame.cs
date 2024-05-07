using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public Text scores;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("player", "");
        PlayerPrefs.SetString("score", "");
        StartCoroutine(GetScores());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_Again()
    {
        SceneManager.LoadScene("Intro");
    }

    IEnumerator GetScores()
    {
        string highscoreURL = "http://localhost/readScores.php?";
        UnityWebRequest hs_get = UnityWebRequest.Get(highscoreURL);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: "
                    + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                for (int i = 0; i < mc.Count; i++)
                {
                    if (i % 2 == 0)
                        scores.text += splitData[i];
                    else
                        scores.text += splitData[i];
                }
            }
        }
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
