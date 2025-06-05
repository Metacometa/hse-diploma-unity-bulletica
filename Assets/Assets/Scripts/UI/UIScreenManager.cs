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
        Time.timeScale = 0;
        animator.SetTrigger("Show");
    }

    public void HideUI()
    {
        animator.SetTrigger("Hide");
    }

    public void OnHideComplete()
    {
        DisableInteractibility();
        Time.timeScale = 1;
    }
    public void OnShowComplete()
    {
        EnableInteractibility();
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
        interactableGroup.blocksRaycasts = true;
    }

    public void DisableInteractibility()
    {
        interactableGroup.interactable = false;
        interactableGroup.blocksRaycasts = false;
    }
}
