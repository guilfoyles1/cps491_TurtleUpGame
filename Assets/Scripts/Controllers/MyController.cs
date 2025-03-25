using UnityEngine;

public class myController : MonoBehaviour
{
    // Original serialized fields (only runSpeed value changed)
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 7f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector2 lastMoveDirection = Vector2.down;
    [SerializeField] GroundChecker groundChecker;

    // Minimal click-to-move addition
    private Vector2? targetPosition = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Original keyboard input handling
        HandleOriginalMovementInput();
        
        // Minimal click-to-move addition
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        // Original animation handling
        UpdateOriginalAnimations();
    }

    void HandleOriginalMovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Cancel click-to-move if keyboard is used
        if (movement != Vector2.zero)
        {
            targetPosition = null;
        }
    }

    void FixedUpdate()
    {
        // Click-to-move integration
        if (targetPosition.HasValue)
        {
            Vector2 direction = (targetPosition.Value - (Vector2)transform.position);
            if (direction.magnitude > 0.1f)
            {
                movement = direction.normalized;
                lastMoveDirection = movement;
            }
            else
            {
                movement = Vector2.zero;
                targetPosition = null;
            }
        }

        // Original movement code (only runSpeed value changed)
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.velocity = movement * speed;
    }

    // Original animation code kept completely intact
    void UpdateOriginalAnimations()
    {
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

        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isWalking", movement != Vector2.zero && !isRunning);
        animator.SetBool("isIdle", movement == Vector2.zero);
        animator.SetFloat("moveX", lastMoveDirection.x);
        animator.SetFloat("moveY", lastMoveDirection.y);
    }
}