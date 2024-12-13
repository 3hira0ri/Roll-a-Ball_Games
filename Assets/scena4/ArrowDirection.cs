using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    public GameObject arrow; // Strzałka, przypisz w inspektorze
    public float offset = 1.5f; // Odległość strzałki od kuli
    
    void Update()
    {
        // Sprawdzamy wejścia gracza
        if (Input.GetKey(KeyCode.A)) // Ruch w lewo
        {
            arrow.transform.position = transform.position + Vector3.right * offset;
            arrow.transform.rotation = Quaternion.Euler(-90, 0, 90); // Ustaw stałą rotację
            arrow.SetActive(true); // Włącz strzałkę
        }
        else if (Input.GetKey(KeyCode.D)) // Ruch w prawo
        {
            arrow.transform.position = transform.position + Vector3.left * offset;
            arrow.transform.rotation = Quaternion.Euler(-90, 180, 90); // Ustaw stałą rotację
            arrow.SetActive(true); // Włącz strzałkę
        }
        else
        {
            arrow.SetActive(false); // Wyłącz strzałkę, gdy nie ma ruchu
        }
    }
}
