using UnityEngine;

public class FloatUpAndFadeOnce : MonoBehaviour
{
    public float floatDistance = 1f;
    public float duration = 1f;

    private Vector3 startLocalPos;
    private SpriteRenderer spriteRenderer;
    private float elapsedTime;
    private bool animating = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startLocalPos = transform.localPosition;
    }

    void OnEnable()
    {
        Restart(); // Always restart when activated
    }

    public void Restart()
    {
        elapsedTime = 0f;
        transform.localPosition = startLocalPos;
        SetAlpha(1f);
        animating = true;
    }

    void Update()
    {
        if (!animating) return;

        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / duration;

        transform.localPosition = startLocalPos + Vector3.up * floatDistance * progress;
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
