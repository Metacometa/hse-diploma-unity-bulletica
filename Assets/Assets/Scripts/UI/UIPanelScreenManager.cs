using UnityEngine;

public class UIPanelScreenManager : UIScreenManager
{
    private UIScreenManager parentUIElement;
    public void ShowUI(UIScreenManager parent)
    {
        parentUIElement = parent;
        parentUIElement.DisableInteractibility();
        animator.SetTrigger("Show");
    }
    public new void OnHideComplete()
    {
        parentUIElement.EnableInteractibility();
    }
}
