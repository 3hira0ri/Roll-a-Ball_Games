using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.SocialPlatforms.Impl;

public class NextLevel : MonoBehaviour
{
    public GameObject NextLevelButton, scorescounter;
    public Text TextScore;
    public Text WinText;
    public int score = 0;
    public Rigidbody body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    public void GiveScore()
    {
        score++;
        TextScore.text = "Score: " + score;
    }
    public void ChangeLevel(int a)
    {
        SceneManager.LoadScene(a, LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        if (score >= scorescounter.transform.childCount)
        {
            TextScore.text = "Score: " + score;
            WinText.gameObject.SetActive(true);
            TextScore.gameObject.SetActive(false);
            body.isKinematic = true;
            NextLevelButton.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Debug.Log("Zdobyles wszystkie punkty (nie jest to zbyt duze wyzwanie)");
        }
    }
}
