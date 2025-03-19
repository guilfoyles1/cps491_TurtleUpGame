using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    private AsyncOperation asyncOp;
    public float fakeLoadDuration = 1f; // Artificial delay in seconds
    private float fakeProgress = 0f;

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

        while (fakeProgress < 1f)
        {
            elapsedTime += Time.deltaTime;

            // Increment the fake progress manually over time
            float targetProgress = Mathf.Clamp01(asyncOp.progress / 0.9f); // Normalize progress
            fakeProgress += Time.deltaTime / fakeLoadDuration; // Manually slow down the progress

            // Use the minimum value between real and fake progress
            loadingSlider.value = Mathf.Min(fakeProgress, targetProgress);

            yield return null;
        }

        yield return new WaitForSeconds(1f); // Extra delay before activation
        asyncOp.allowSceneActivation = true;
    }
}
