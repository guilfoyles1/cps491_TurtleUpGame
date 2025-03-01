using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public LayerMask foregroundLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        // collision check
        Vector2 moveDirection = moveInput * moveSpeed * Time.fixedDeltaTime;
        if (!IsCollidingWithForeground(moveDirection))
        {
            rb.velocity = moveInput * moveSpeed; // no collision
        }
        else
        {
            rb.velocity = Vector2.zero; // collision
        }
    }

    bool IsCollidingWithForeground(Vector2 moveDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, moveDirection, moveDirection.magnitude, foregroundLayer);
        return hit.collider != null;
    }
}
