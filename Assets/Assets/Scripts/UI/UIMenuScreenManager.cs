using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuScreenManager : UIScreenManager
{
    private bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                TogglePause();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = true;
        ShowUI();
    }
    public void ResumeGame()
    {
        isPaused = false;
        HideUI();
    }
}
