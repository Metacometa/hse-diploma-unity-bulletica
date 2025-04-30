using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndScreenManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Show");
    }

    public void HideUI()
    {
        animator.SetTrigger("Hide"); 
    }

    public void OnHideComplete()
    {
        gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(1); 
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); 
    }
}
