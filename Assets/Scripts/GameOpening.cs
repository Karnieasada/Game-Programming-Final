using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOpening : MonoBehaviour
{
    public Text playername;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        PlayerPrefs.SetString("player", playername.text);
        PlayerPrefs.SetString("score", "0");
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
