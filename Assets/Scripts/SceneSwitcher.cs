using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher: MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For in-editor quitting
#endif
    }
}