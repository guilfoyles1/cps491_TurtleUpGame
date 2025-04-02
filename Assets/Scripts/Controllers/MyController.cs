using UnityEngine;
using UnityEngine.EventSystems;

public class myController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float acceleration = 12f; // Added for smooth transitions
    [SerializeField] float deceleration = 15f;
    
    [Header("Click-to-Move Settings")]
    [SerializeField] bool allowRunOnClick = true;
    [SerializeField] float clickRunStopDistance = 0.2f;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 targetVelocity;
    private Vector2 currentVelocity;
    private Vector2 lastMoveDirection = Vector2.down;
    [SerializeField] GroundChecker groundChecker;
    
    // Movement state
    private Vector2? targetPosition = null;
    private bool isClickRunning = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleKeyboardInput();
        HandleClickInput();
        UpdateAnimations();
    }

    void HandleKeyboardInput()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        if (input != Vector2.zero)
        {
            targetPosition = null; // Cancel click-to-move
            targetVelocity = input * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
        }
        else if (!targetPosition.HasValue)
        {
            targetVelocity = Vector2.zero;
        }
    }

void HandleClickInput()
{
    // Prevent click-to-move when clicking on UI
    if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
    {
        return;
    }

    if (Input.GetMouseButtonDown(0))
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Double-click to run (if enabled)
        if (allowRunOnClick && Time.time - lastClickTime < 0.3f)
        {
            isClickRunning = !isClickRunning;
        }

        lastClickTime = Time.time;
    }
}
    private float lastClickTime;

    void FixedUpdate()
    {
        if (targetPosition.HasValue)
        {
            Vector2 direction = (targetPosition.Value - (Vector2)transform.position);
            float distance = direction.magnitude;
            
            if (distance > clickRunStopDistance)
            {
                float speed = isClickRunning ? runSpeed : walkSpeed;
                targetVelocity = direction.normalized * speed;
            }
            else
            {
                targetVelocity = Vector2.zero;
                targetPosition = null;
            }
        }

        // Smooth movement
        currentVelocity = Vector2.Lerp(
            currentVelocity,
            targetVelocity,
            (targetVelocity.magnitude > 0.1f ? acceleration : deceleration) * Time.fixedDeltaTime
        );
        
        rb.velocity = currentVelocity;
    }

    void UpdateAnimations()
    {
        bool isMoving = currentVelocity.magnitude > 0.1f;
        bool isRunning = (Input.GetKey(KeyCode.LeftShift) || isClickRunning) && isMoving;

        if (isMoving)
        {
            lastMoveDirection = currentVelocity.normalized;
            groundChecker.PlayWalkSound();
        }
        else
        {
            groundChecker.StopWalkSound();
        }

        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isIdle", !isMoving);
        animator.SetFloat("moveX", lastMoveDirection.x);
        animator.SetFloat("moveY", lastMoveDirection.y);
    }
}