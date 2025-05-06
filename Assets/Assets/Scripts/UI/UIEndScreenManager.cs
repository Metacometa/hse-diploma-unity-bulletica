using System.Collections.Generic;
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
        //DisableButtons();

        gameObject.SetActive(false);
        //animator.SetTrigger("Hide");
    }

    public void ShowUI()
    {
		Time.timeScale = 0;
        //gameObject.SetActive(true);

        EnableButtons();
        animator.SetTrigger("Show");
    }

    public void HideUI()
    {
		Time.timeScale = 1;
        animator.SetTrigger("Hide"); 
    }

    public void OnHideComplete()
    {
        //gameObject.SetActive(false);
        DisableButtons();
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

    public void DisableButtons()
    {
        foreach(Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }
}
