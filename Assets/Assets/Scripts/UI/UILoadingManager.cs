using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text; 
    [SerializeField] private float speed = 0.5f;

    private Coroutine animCoroutine;
    public void LoadScene(int sceneIndex)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    private void OnEnable()
    {
        animCoroutine = StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        while (true)
        {
            for (int i = 0; i <= 3; i++)
            {
                text.text = "Загрузка" + new string('.', i);
                yield return new WaitForSeconds(speed);
            }
        }
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false; 

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2f);
                if (animCoroutine != null)
                {
                    StopCoroutine(animCoroutine);
                }
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}