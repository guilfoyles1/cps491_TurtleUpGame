using UnityEngine;

public class myController : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMoveDirection = Vector2.down;

    public GroundChecker groundChecker; // Reference to GroundChecker

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && movement != Vector2.zero;

        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement;
            groundChecker.PlayWalkSound();
        }
        else
        {
            groundChecker.StopWalkSound();
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
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.velocity = movement * speed;
    }
}
