using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private float dotSpeed = 0.5f;
    [SerializeField] private int maxDots = 3;
    [SerializeField] private float minLoadTime = 2f;

    private string baseText = "Загрузка";
    private float timer;
    private int currentDots;
    private float loadStartTime;
    private AsyncOperation asyncLoad;
    private bool isLoading;

    private void Update()
    {
        if (!isLoading) return;

        timer += Time.deltaTime;
        if (timer >= dotSpeed)
        {
            timer = 0;
            currentDots = (currentDots + 1) % (maxDots + 1);
            loadingText.text = baseText + new string('.', currentDots);
        }

        if (asyncLoad != null && asyncLoad.progress >= 0.9f &&
            Time.time - loadStartTime >= minLoadTime)
        {
            CompleteLoading();
        }
    }

    public void LoadScene(int sceneIndex)
    {
        gameObject.SetActive(true);
        isLoading = true;
        loadStartTime = Time.time;
        timer = 0;
        currentDots = 0;
        loadingText.text = baseText;

        asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;
    }

    private void CompleteLoading()
    {
        isLoading = false;
        asyncLoad.allowSceneActivation = true;
    }

    private void OnDisable()
    {
        if (asyncLoad != null && !asyncLoad.isDone)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }
}