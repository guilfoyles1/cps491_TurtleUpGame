using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class SquareController : MonoBehaviour
{
    public float moveSpeed = 50f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public GameObject popupUI; // Reference to the popup UI
    public string targetTag = "TargetObject"; // Tag of the object you want to collide with

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

        if (popupUI != null)
        {
            popupUI.SetActive(false); // Hide the popup initially
        }
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // Flip sprite based on movement direction
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true; // Facing left
        }
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveDirection = (mousePosition - (Vector2)transform.position).normalized * moveSpeed;

            // Flip sprite based on mouse position
            spriteRenderer.flipX = mousePosition.x < transform.position.x;
        }
        else if (moveInput != Vector2.zero)
        {
            moveDirection = moveInput * moveSpeed;
        }

        rb.velocity = moveDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            ShowPopup("You collided with the target object!");
            Destroy(collision.gameObject);
        }
    }

    void ShowPopup(string message)
    {
        if (popupUI != null)
        {
            popupUI.SetActive(true); // Show the popup
        }
    }

    // Optionally hide the popup when the player clicks a button
    public void HidePopup()
    {
        if (popupUI != null)
        {
            popupUI.SetActive(false);
        }
    }
}
