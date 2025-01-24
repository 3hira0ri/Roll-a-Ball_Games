using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    [SerializeField] Canvas canvas; // Przypisz Canvas w Inspectorze
    [SerializeField] Canvas eq; // Przypisz Canvas w Inspectorze

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // SprawdŸ, czy u¿ytkownik wcisn¹³ klawisz M
        if (Input.GetKeyDown(KeyCode.M))
        {

            if (canvas != null)
            {
                Debug.Log(canvas.enabled);

                if (!Cursor.visible)
                {
                    canvas.gameObject.SetActive(true);
                    eq.gameObject.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    canvas.gameObject.SetActive(false);
                    eq.gameObject.SetActive(true);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                // Prze³¹cz widocznoœæ Canvas
                
            }
        }
    }
}
