using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject MenuMain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        Debug.Log("start");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void settings(bool isActive)
    {
        OptionsPanel.SetActive(isActive);
        MenuMain.SetActive(!isActive);
    }
    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
