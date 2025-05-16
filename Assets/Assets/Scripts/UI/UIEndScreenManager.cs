using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEndScreenManager : MonoBehaviour
{
    private Animator animator;
    private Button[] buttons;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        buttons = GetComponentsInChildren<Button>();
        DisableInteractibility();
    }

    public void ShowUI()
    {
		//Time.timeScale = 0;
        animator.SetTrigger("Show");
    }

    public void HideUI()
    {
		//Time.timeScale = 1;
        animator.SetTrigger("Hide");
    }

    public void OnHideComplete()
    {
        DisableInteractibility();
    }
    public void OnShowComplete()
    {
        EnableInteractibility();
    }

    public void RestartGame()
    {
        //Time.timeScale = 1; 
        SceneManager.LoadScene(1); 
    }

    public void ReturnToMenu()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(0); 
    }

    public void EnableInteractibility()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void DisableInteractibility()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
}
