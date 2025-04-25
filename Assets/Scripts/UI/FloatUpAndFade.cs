using UnityEngine;

public class FloatUpAndFadeOnce : MonoBehaviour
{
    public float floatDistance = 1f;
    public float duration = 1f;
    public float spawnRadius = 1.5f; // in pixels (Unity units depend on canvas or world scale)

    private Vector3 baseLocalPos;
    private Vector3 offset;
    private SpriteRenderer spriteRenderer;
    private float elapsedTime;
    private bool animating = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseLocalPos = transform.localPosition;
    }

    void OnEnable()
    {
        Restart();
    }

    public void Restart()
    {
        elapsedTime = 0f;

        // 🎲 Random X/Y offset within radius
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        offset = new Vector3(randomOffset.x, randomOffset.y, 0);

        transform.localPosition = baseLocalPos + offset;
        SetAlpha(1f);
        animating = true;
    }

    void Update()
    {
        if (!animating) return;

        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / duration;

        transform.localPosition = baseLocalPos + offset + Vector3.up * floatDistance * progress;
        SetAlpha(1f - progress);

        if (progress >= 1f)
        {
            animating = false;
            gameObject.SetActive(false);
        }
    }

    void SetAlpha(float a)
    {
        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = a;
            spriteRenderer.color = c;
        }
    }
}
