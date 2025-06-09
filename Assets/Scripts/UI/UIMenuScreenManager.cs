using UnityEngine;

public class UIMenuScreenManager : UIScreenManager
{
    private bool isPaused = false;
    [SerializeField] private UISettingsPanelScreenManager settingsScreenPanel;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isBlocked)
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
        if (!isOpen)
        {
            isPaused = true;
            ShowUI();
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
        HideUI();
    }
    public void SettingsPanelScreen()
    {
        settingsScreenPanel.ShowUI(this);
        isBlocked = true;
    }
}
