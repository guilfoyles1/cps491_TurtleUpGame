using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic; // For List<T>

public class playerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public LayerMask foregroundLayer;
    public float doubleClickTime = 0.3f; // Time window for double click
    public float interactionRadius = 1f; // Radius for interacting with trash
    public int score = 0; // Score variable

    public Tilemap trashTilemap; // Reference to the tilemap containing trash tiles
    public List<TileBase> trashTiles; // List of trash tiles (assign in Inspector)

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 targetPosition;
    private bool isMovingToTarget = false;
    private float lastClickTime = 0f; // Track the time of the last click

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // get spriterenderer component
        animator = GetComponent<Animator>(); //get animator component
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
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickTime)
            {
                // Double click detected
                Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TryInteractWithTrash(clickPosition);
            }
            else
            {
                // Single click detected
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isMovingToTarget = true;
            }

            lastClickTime = Time.time; // Update the last click time
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
            //start running animation
            animator.SetBool("isRunning", moveInput.magnitude > 0); //start running animation only during movement input
            rb.velocity = moveInput * moveSpeed; // move - no collision

            //flip sprite based on movement direction
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false; //facing right
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true; //facing left
            }

        }
        else
        {
            animator.SetBool("isRunning", false); //stop run animation
            rb.velocity = Vector2.zero; // collision
        }
    }

    void MoveToTarget()
    {
        Vector2 moveDirection = (targetPosition - rb.position).normalized;

        // collision check
        if (!IsCollidingWithForeground(moveDirection))
        {
            animator.SetBool("isRunning", true); //start run animation
            rb.velocity = moveDirection * moveSpeed;

            //flip sprite based on movement direction
            if (moveDirection.x > 0)
            {
                spriteRenderer.flipX = false; //facing right
            }
            else if (moveDirection.x < 0)
            {
                spriteRenderer.flipX = true; //facing left
            }
        }
        else
        {
            animator.SetBool("isRunning", false); //stop run animation
            rb.velocity = Vector2.zero; // stop - collision
            isMovingToTarget = false;
        }

        // stop movement when near target
        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            animator.SetBool("isRunning", false); //stop run animation
            rb.velocity = Vector2.zero;
            isMovingToTarget = false;
        }
    }

    bool IsCollidingWithForeground(Vector2 moveDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, moveDirection, moveDirection.magnitude, foregroundLayer);
        return hit.collider != null;
    }

    void TryInteractWithTrash(Vector2 clickPosition)
    {
        // Convert the click position to tilemap coordinates
        Vector3Int cellPosition = trashTilemap.WorldToCell(clickPosition);

        // Check if the clicked cell contains a trash tile
        TileBase clickedTile = trashTilemap.GetTile(cellPosition);
        if (clickedTile != null && trashTiles.Contains(clickedTile))
        {
            // Check if the player is close enough to the trash
            Vector2 trashWorldPosition = trashTilemap.CellToWorld(cellPosition);
            float distanceToTrash = Vector2.Distance(rb.position, trashWorldPosition);

            if (distanceToTrash <= interactionRadius)
            {
                // Remove the trash tile
                trashTilemap.SetTile(cellPosition, null);
                score++; // Increment score
                Debug.Log("Trash collected! Score: " + score);
            }
        }
    }
}