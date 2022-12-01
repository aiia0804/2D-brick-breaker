using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadNextScene()
    {
        int currentSceneInedex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneInedex + 1);

    }
    public void LoadStartScene()
    {
        Destroy(FindObjectOfType<MusicPlayer>().gameObject);
        SceneManager.LoadScene(0);
        FindObjectOfType<GameStatus>().ResetGame();
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene("Game Over");
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
