using UnityEngine;
using UnityEngine.UI;

public class Czas : MonoBehaviour
{
    public Text Czasow;
    public GameObject Player;
    private float startTime;
    private bool isTimerRunning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = 0f;
        isTimerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.GetComponent<Rigidbody>().isKinematic && !isTimerRunning) //start gdy gracz ma kontrole
        {
            startTime = Time.fixedUnscaledTime;
            isTimerRunning = true;
        }
        else if(Player.GetComponent<Rigidbody>().isKinematic)
        {
            isTimerRunning = false;
        }

    }
    private void FixedUpdate()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.fixedUnscaledTime - startTime;
            Czasow.text = elapsedTime.ToString("F2"); // Wyświetlanie czasu z dokładnością do dwóch miejsc po przecinku
        }
    }
}
