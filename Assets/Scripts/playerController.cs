using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public LayerMask foregroundLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 targetPosition;
    private bool isMovingToTarget = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position; // start - current pos
    }

    void Update()
    {
        // wasd movement
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        // click movement
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMovingToTarget = true;
        }
    }

    void FixedUpdate()
    {
        if (isMovingToTarget)
        {
            MoveToTarget();
        }
        else
        {
            MoveWithKeys();
        }
    }

    void MoveWithKeys()
    {
        Vector2 moveDirection = moveInput * moveSpeed * Time.fixedDeltaTime;
        if (!IsCollidingWithForeground(moveDirection))
        {
            rb.velocity = moveInput * moveSpeed; // move - no collision
        }
        else
        {
            rb.velocity = Vector2.zero; // collision
        }
    }

    void MoveToTarget()
    {
        Vector2 moveDirection = (targetPosition - rb.position).normalized;

        // collision check
        if (!IsCollidingWithForeground(moveDirection))
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // stop - collision
            isMovingToTarget = false;
        }

        // stop movement when near target
        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            rb.velocity = Vector2.zero;
            isMovingToTarget = false;
        }
    }

    bool IsCollidingWithForeground(Vector2 moveDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, moveDirection, moveDirection.magnitude, foregroundLayer);
        return hit.collider != null;
    }
}
