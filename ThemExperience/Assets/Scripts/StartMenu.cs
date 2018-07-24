using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public void StartTheGame()
    {
        PlayerPrefs.SetInt("Levels", 1);
        SceneManager.LoadScene("Level1");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
