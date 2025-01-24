using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] StartGame game;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject MenuMain;
    [SerializeField] Quit exit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        game.ChangeLevel(1);
    }
    public void settings(bool isActive)
    {
        OptionsPanel.SetActive(isActive);
        MenuMain.SetActive(!isActive);
    }
    public void Exit()
    {
        exit.exit();
    }
}
