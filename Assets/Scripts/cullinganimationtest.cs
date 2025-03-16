using UnityEngine;

public class AnimationCullingTest : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.1f)
        {
            Debug.Log($"Animation is playing at {Time.time}");
        }
    }
}
