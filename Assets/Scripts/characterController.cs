using UnityEngine;

public class SquareController : MonoBehaviour
{
    public float moveSpeed = 50f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveDirection = (mousePosition - (Vector2)transform.position).normalized * moveSpeed;
        }
        else if (moveInput != Vector2.zero)
        {
            moveDirection = moveInput * moveSpeed;
        }

        rb.velocity = moveDirection;
    }
}
