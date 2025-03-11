using UnityEngine;

public class myController : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 7f; // Faster sprint speed
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMoveDirection = Vector2.down; // Default facing down

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize movement to prevent diagonal speed boost
        movement = movement.normalized;

        // Sprinting when holding Left Shift
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && movement != Vector2.zero;

        // If moving, update lastMoveDirection
        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement;
        }

        // Handle animation states
        if (isRunning)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);
        }
        else if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        // Set animator movement parameters
        animator.SetFloat("moveX", lastMoveDirection.x);
        animator.SetFloat("moveY", lastMoveDirection.y);
    }

    void FixedUpdate()
    {
        // Apply movement speed (running or walking)
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.velocity = movement * speed;
    }
}
