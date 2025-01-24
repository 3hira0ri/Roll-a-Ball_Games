using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame:MonoBehaviour
{
    public void ChangeLevel(int a)
    {
        SceneManager.LoadScene(a, LoadSceneMode.Single);
    }

}
