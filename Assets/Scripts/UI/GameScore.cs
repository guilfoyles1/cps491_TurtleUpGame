using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public int score = 0;
    public Slider scoreBar;
    public int maxScore = 50000;

    //score gain text
    public TextMeshProUGUI scoreGainText;
    private Coroutine scoreTextRoutine;


    //speed bonus
    private float lastCorrectTime = -10f;
    public float bonusWindow = .5f; // Time allowed between correct drops for a bonus
    public int speedBonus = 200;


    //streak multiplier
    private int streakCount = 0;
    public float streakWindow = 1f; // Time allowed between actions to continue streak
    public int baseScore = 1000;
    public float maxMultiplier = 1.6f;


    void Start()
    {
        UpdateScoreUI();
    }

    public void AddStreakedScore(bool checkSpeedBonus = false)
    {
        float currentTime = Time.time;

        streakCount++;

        float multiplier = GetMultiplier();
        int baseAward = Mathf.RoundToInt(baseScore * multiplier);
        int totalScore = baseAward;

        // Handle speed bonus
        if (checkSpeedBonus && (currentTime - lastCorrectTime <= bonusWindow))
        {
            totalScore += speedBonus;
            Debug.Log("Speed Bonus! + " + speedBonus);
        }

        score += totalScore;
        Debug.Log($"Streak x{streakCount} | Multiplier: x{multiplier:F1} | +{totalScore} points");

        lastCorrectTime = currentTime;

        // Show floating score gain text
        if (scoreTextRoutine != null)
            StopCoroutine(scoreTextRoutine);
        scoreTextRoutine = StartCoroutine(ShowScoreGain(totalScore));


        UpdateScoreUI();
    }



    public void SubtractScore(int amount)
    {
        score -= amount;
        score = Mathf.Max(score, 0);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreBar != null)
        {
            scoreBar.value = Mathf.Clamp(score, 0, maxScore);
        }
    }


    private float GetMultiplier()
    {
        return Mathf.Min(1f + (streakCount - 1) * 0.2f, maxMultiplier);
    }

    public void ResetStreak()
    {
        streakCount = 0;
        Debug.Log("Streak broken.");
    }

    private IEnumerator ShowScoreGain(int amount)
    {
        scoreGainText.text = $"+{amount}";
        Color startColor = scoreGainText.color;
        startColor.a = 1f;
        scoreGainText.color = startColor;

        Vector3 originalPos = scoreGainText.rectTransform.localPosition;

        float duration = 0.6f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Fade out and move up
            float t = elapsed / duration;
            scoreGainText.color = new Color(startColor.r, startColor.g, startColor.b, 1f - t);
            scoreGainText.rectTransform.localPosition = originalPos + new Vector3(0, t * 30f, 0); // float up 30 units

            yield return null;
        }

        // Reset
        scoreGainText.text = "";
        scoreGainText.rectTransform.localPosition = originalPos;
    }


}
