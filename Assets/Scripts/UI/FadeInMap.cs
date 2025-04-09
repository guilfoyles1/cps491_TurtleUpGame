using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FadeInMap : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 2f; // How long the fade takes
    [SerializeField] float delayBeforeFade = 1f; // Delay before fade starts

    void Awake()
    {
        if (canvasGroup == null) Debug.Log("fade in canvas not found");
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
    }
    void Start()
    {

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delayBeforeFade); // Wait before starting fade

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false); // Disable panel after fade-out
    }
}
