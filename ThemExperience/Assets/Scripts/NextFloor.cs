using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloor : MonoBehaviour {

    public string sceneName;
    GameObject Spawner;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        int currentScene = PlayerPrefs.GetInt("Levels");
        Transform hit = collision.gameObject.transform;
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Levels", currentScene + 1);
            SceneManager.LoadScene("Level" + (currentScene + 1));
            //  Scene newLevel = SceneManager.CreateScene("Level" + (currentScene + 1));
          //  PlayerPrefs.SetInt("Levels", currentScene);           
           // SceneManager.LoadScene(newLevel.name);
        }


    }
}
