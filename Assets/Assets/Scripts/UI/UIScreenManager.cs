using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScreenManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    private Animator animator;
    private CanvasGroup interactableGroup;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        interactableGroup = GetComponentInChildren<CanvasGroup>();
        DisableInteractibility();
    }

    public void ShowUI()
    {
        animator.SetTrigger("Show");
    }

    public void HideUI()
    {
        Time.timeScale = 1;
        animator.SetTrigger("Hide");
    }

    public void OnHideComplete()
    {
        DisableInteractibility();
    }
    public void OnShowComplete()
    {
        EnableInteractibility();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        loadingScreen.LoadScene(1); 
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        loadingScreen.LoadScene(0); 
    }

    public void EnableInteractibility()
    {
        interactableGroup.interactable = true;
    }

    public void DisableInteractibility()
    {
        interactableGroup.interactable = false;
    }
}
