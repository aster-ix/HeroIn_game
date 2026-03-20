using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public int GameScene = 1;
    
    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
