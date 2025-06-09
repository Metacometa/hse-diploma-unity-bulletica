using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    public void Play()
    {
        loadingScreen.LoadScene(1); 
    }

    public void Quit()
    {
        Application.Quit(); 

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
