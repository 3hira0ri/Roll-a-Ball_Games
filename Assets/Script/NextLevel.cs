using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    [SerializeField] GameObject NextLevelButton, scorescounter;
    [SerializeField] Text TextScore;
    [SerializeField] Text WinText;
    [SerializeField] int score = 0;
    [SerializeField] Rigidbody body;
    [SerializeField] GameObject Finished;
    [SerializeField] int neededScore;
    [SerializeField] GameObject[] collectible;
    [SerializeField] bool isSync=true;
    [SerializeField] StartGame game;
    void Start()
    {
       collectible = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (var score in collectible)
        {
            score.GetComponent<Collectible>().PickUpEvent += GiveScore;
        }
        if (isSync)
        {
            neededScore = collectible.Length;
        }
        else
        {
            neededScore = 1;
        }
    }
    public void GiveScore()
    {
        score++;
        TextScore.text = "Score: " + score;
    }
    public void StartGame(int a)
    {
       game.ChangeLevel(a);
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
