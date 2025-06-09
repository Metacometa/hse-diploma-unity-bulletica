using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScreenManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    protected Animator animator;
    protected static bool isOpen;
    protected bool isBlocked;
    protected CanvasGroup interactableGroup;

    protected virtual void Awake()
    {
        isOpen = false;
        isBlocked = false;
        animator = GetComponent<Animator>();
        interactableGroup = GetComponentInChildren<CanvasGroup>();
        DisableInteractibility();
    }

    public void ShowUI()
    {
        if (!isOpen)
        {
            isOpen = true;
            Time.timeScale = 0;
            animator.SetTrigger("Show");
        }
    }

    public void HideUI()
    {
        DisableInteractibility();
        animator.SetTrigger("Hide");
    }

    public void OnHideComplete()
    {
        Time.timeScale = 1;
        isOpen = false;
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
        isBlocked = false;
    }

    public void DisableInteractibility()
    {
        interactableGroup.interactable = false;
        interactableGroup.blocksRaycasts = false;
    }
}
