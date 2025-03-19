using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SunlightManager : MonoBehaviour
{
    public CanvasGroup[] gradientImages; // Assign all 3 gradient images in Inspector
    public float transitionSpeed = 2.0f; // Speed of transition
    private int currentIndex = 0; // Tracks which image is currently active

    void Awake()
    {
        // Ensure images 5 and 6 are active at the start
        for (int i = 1; i < gradientImages.Length; i++)
        {
            gradientImages[i].gameObject.SetActive(true);
        }
    }

    void Start()
    {
        // Ensure only the first image is fully visible at start
        for (int i = 0; i < gradientImages.Length; i++)
        {
            gradientImages[i].alpha = (i == 0) ? 1 : 0;
        }

        // Start the transition loop
        StartCoroutine(TransitionLoop());
    }

    IEnumerator TransitionLoop()
    {
        while (true)
        {
            int nextIndex = (currentIndex + 1) % gradientImages.Length;
            yield return StartCoroutine(FadeTransition(gradientImages[currentIndex], gradientImages[nextIndex]));
            currentIndex = nextIndex;
        }
    }

    IEnumerator FadeTransition(CanvasGroup fadeOut, CanvasGroup fadeIn)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * transitionSpeed;
            fadeOut.alpha = Mathf.Lerp(1, 0, t);
            fadeIn.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }
    }
}
