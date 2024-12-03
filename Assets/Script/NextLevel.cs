using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
public class NextLevel : MonoBehaviour
{
    public GameObject NextLevelButton, scorescounter;
    public Text TextScore;
    public Text WinText;
    public int score = 0;
    public Rigidbody body;
    public GameObject Finished;
    public int neededScore;
    public GameObject[] collectible;
    void Start()
    {
       collectible = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (var score in collectible)
        {
            score.GetComponent<Collectible>().PickUpEvent += GiveScore;
        }

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
    void Update()
    {
        Scored();
    }
    void Scored()
    {
        if (score >= neededScore && !Finished.active)
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
