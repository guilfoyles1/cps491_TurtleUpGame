<<<<<<< HEAD
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingSlider;
    [SerializeField] GameObject creditsPanel; // <-- NEW

    private AsyncOperation asyncOp;
    private float loadDuration = 1f;
    private float progress = 0f;

    public void startGame()
    {
        loadingScreen.SetActive(true);
        loadingSlider.gameObject.SetActive(true);
        StartCoroutine(LoadSceneWithDelay(1));
    }

    IEnumerator LoadSceneWithDelay(int sceneIndex)
    {
        asyncOp = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOp.allowSceneActivation = false;

        float elapsedTime = 0f;

        while (progress < 1f)
        {
            elapsedTime += Time.deltaTime;

            float targetProgress = Mathf.Clamp01(asyncOp.progress / 0.9f);
            progress += Time.deltaTime / loadDuration;

            loadingSlider.value = Mathf.Min(progress, targetProgress);

            yield return null;
        }

        yield return new WaitForSeconds(.5f);
        asyncOp.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ----- NEW STUFF -----

    public void ShowCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }
}
=======
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingSlider;
    private AsyncOperation asyncOp;
    private float loadDuration = 1f;
    private float progress = 0f;

    public void startGame()
    {
        loadingScreen.SetActive(true);
        loadingSlider.gameObject.SetActive(true);
        StartCoroutine(LoadSceneWithDelay(1));
    }

    IEnumerator LoadSceneWithDelay(int sceneIndex)
    {
        asyncOp = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOp.allowSceneActivation = false; // Prevent instant scene activation

        float elapsedTime = 0f;

        while (progress < 1f)
        {
            elapsedTime += Time.deltaTime;

            // Increment the progress over time
            float targetProgress = Mathf.Clamp01(asyncOp.progress / 0.9f); // Normalize progress
            progress += Time.deltaTime / loadDuration;

            loadingSlider.value = Mathf.Min(progress, targetProgress);

            yield return null;
        }

        yield return new WaitForSeconds(.5f);
        asyncOp.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
>>>>>>> 170d36684f19b92e12997d1a1e72fd5da00dcd84
