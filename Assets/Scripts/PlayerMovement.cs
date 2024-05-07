using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour
{
    public GameObject reward;
    public float movement = 4.0f;
    public float speed = 1.0f;
    public int score = 0;
    public Text scoreText;
    private string secretKey = "mySecretKey";
    public string addScoreURL = "http://localhost/writeScore.php?";

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveBack();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        this.transform.Translate(speed * movement * Time.deltaTime * Vector3.left);
    }

    public void MoveRight()
    {
        this.transform.Translate(speed * movement * Time.deltaTime * Vector3.right);
    }

    private void MoveForward()
    {
        this.transform.Translate(speed * movement * Time.deltaTime * Vector3.forward);
    }

    private void MoveBack()
    {
        this.transform.Translate(speed * movement * Time.deltaTime * Vector3.back);
    }

    private void UpdateScoreText()
    {
        // Update the UI Text with the current score
        scoreText.text = score.ToString();
        if (scoreText.text == "5")
        {
            PlayerPrefs.SetString("score", scoreText.text);
            string name = PlayerPrefs.GetString("player");
            int num_Score = Convert.ToInt32(PlayerPrefs.GetString("score"));
            StartCoroutine(PostScores(name, num_Score));
            SceneManager.LoadScene("Exit");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("reward"))
        {
            score++;
            UpdateScoreText();
            Destroy(other.gameObject);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator PostScores(string name, int score)
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
