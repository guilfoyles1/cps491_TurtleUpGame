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

    void Start()
    {
        UpdateScoreUI();
        mat = scoreText.fontMaterial;
        defaultOutlineColor = mat.GetColor(ShaderUtilities.ID_OutlineColor);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        FlashOutline(Color.green);
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
