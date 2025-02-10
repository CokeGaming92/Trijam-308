using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    public string sceneName; // Name of the scene to reload

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
