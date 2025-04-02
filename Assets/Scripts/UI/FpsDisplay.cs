using TMPro;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fpsText;

    private float timer;
    private int frameCount;

    void Update()
    {
        frameCount++;
        timer += Time.unscaledDeltaTime;

        if (timer >= 1f)
        {
            int fps = Mathf.RoundToInt(frameCount / timer);
            fpsText.text = "FPS: " + fps;

            // Reset for the next interval
            timer = 0f;
            frameCount = 0;
        }
    }
}
