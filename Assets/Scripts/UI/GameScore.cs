using System.Collections;
using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    private Coroutine flashRoutine;
    private Material mat;
    private Color defaultOutlineColor;


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
        mat = scoreText.fontMaterial;
        defaultOutlineColor = mat.GetColor(ShaderUtilities.ID_OutlineColor);
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
            FlashOutline(Color.cyan);
        }
        else
        {
            FlashOutline(Color.green);
        }

        score += totalScore;
        Debug.Log($"Streak x{streakCount} | Multiplier: x{multiplier:F1} | +{totalScore} points");

        lastCorrectTime = currentTime;

        UpdateScoreUI();
    }



    public void SubtractScore(int amount)
    {
        score -= amount;
        score = Mathf.Max(score, 0);
        UpdateScoreUI();
        FlashOutline(Color.red);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
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



    public void FlashOutline(Color flashColor)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            // Immediately reset to default in case coroutine didn't finish
            mat.SetColor(ShaderUtilities.ID_OutlineColor, defaultOutlineColor);
        }
        flashRoutine = StartCoroutine(FlashOutlineCoroutine(flashColor));
    }

    private IEnumerator FlashOutlineCoroutine(Color flashColor)
    {
        mat.SetColor(ShaderUtilities.ID_OutlineColor, flashColor);
        yield return new WaitForSeconds(0.7f);
        mat.SetColor(ShaderUtilities.ID_OutlineColor, defaultOutlineColor);
    }
}
