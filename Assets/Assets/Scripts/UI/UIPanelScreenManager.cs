using UnityEngine;

public class UIPanelScreenManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    private Animator animator;
    private CanvasGroup interactableGroup;

    protected virtual void Awake()
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
