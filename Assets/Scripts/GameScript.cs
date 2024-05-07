using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameScript : MonoBehaviour
{
    public Text playerName;
    public Dropdown playerColor;
    public Slider playerSize;
    public GameObject player;
    public Renderer playerRenderer;
    public GameObject reward;
    public GameObject Pause;
    public GameObject Selection;
    private float player_Scale;
    private string secretKey = "mySecretKey";
    public string addScoreURL = "http://localhost/writeScore.php?";

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PlayerPrefs.GetString("player");
        float x, y, z;
        for (int i = 0; i < 5; i++)
        {
            x = UnityEngine.Random.Range(-4, 4);
            y = UnityEngine.Random.Range(0, 4);
            z = UnityEngine.Random.Range(-4, 4);
            GameObject thing = Instantiate(reward);
            Vector3 spawn = new Vector3(x, y, z);
            thing.transform.position = spawn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerColor.value == 0)
        {
            playerRenderer = player.GetComponent<Renderer>();
            playerRenderer.material.color = Color.green;
        }
        if (playerColor.value == 1)
        {
            playerRenderer = player.GetComponent<Renderer>();
            playerRenderer.material.color = Color.blue;
        }
        if (playerColor.value == 2)
        {
            playerRenderer = player.GetComponent<Renderer>();
            playerRenderer.material.color = Color.red;
        }
        if (Input.GetKey(KeyCode.P))
        {
            Selection.SetActive(false);
            Pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void player_Size()
    {
        player_Scale = playerSize.value;
        Vector3 scale = new Vector3(player_Scale, player_Scale, player_Scale);
        player.transform.localScale = scale;
    }

    public void continue_Game()
    {
        Selection.SetActive(true);
        Pause.SetActive(false);
        Time.timeScale = 1;
    }
    public void Stop()
    {
        string score = GameObject.Find("ScoreText").GetComponent<Text>().text;
        int num_Score = Convert.ToInt32(score);
        StartCoroutine(PostScores(playerName.text, num_Score));
        SceneManager.LoadScene("Exit");
    }

    public IEnumerator PostScores(string name, int score)
    {
        string hash = HashInput(name + score + secretKey);
        string post_url = addScoreURL + "Name=" +
               UnityWebRequest.EscapeURL(name) + "&Score="
               + score + "&hash=" + hash;
        UnityWebRequest hs_post = UnityWebRequest.PostWwwForm(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: "
                    + hs_post.error);
    }

    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue =
                hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert =
                 BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }
}
