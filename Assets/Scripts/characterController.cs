using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquareController : MonoBehaviour
{
    public float moveSpeed = 50f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    public GameObject popupUI;
    public GameObject popupUIText;
    private TextMeshProUGUI _popupUIText;
    public GameObject[] hearts = new GameObject[5];
    public string targetTag = "TargetObject";
    public string targetTag2 = "TargetObject2";
    public string targetTag3 = "TargetObject3";
    private int health = 5;
    private int foodEaten = 0;
    public Sprite heartFull;
    public Sprite heartEmpty;
    public GameObject foodSpawner;
    public GameObject garbageSpawner;
    public Scrollbar progressBar;
    public GameObject sign;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _popupUIText = popupUIText.GetComponent<TextMeshProUGUI>();

        if (popupUI != null)
        {
            popupUI.SetActive(false);
        }
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

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
        SpriteRenderer signRenderer = sign.GetComponent<SpriteRenderer>();

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveDirection = (mousePosition - (Vector2)transform.position).normalized * moveSpeed;

            spriteRenderer.flipX = mousePosition.x < transform.position.x;
            animator.SetBool("IsSwimming", true);
            signRenderer.enabled = false;
        }
        else if (moveInput != Vector2.zero)
        {
            moveDirection = moveInput * moveSpeed;
            animator.SetBool("IsSwimming", true);
            signRenderer.enabled = false;
        }
        else
        {
            animator.SetBool("IsSwimming", false);
        }

        rb.velocity = moveDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            SpriteRenderer spriteRenderer = hearts[health-1].GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = heartEmpty;
            Destroy(collision.gameObject);
            health = health-1;
            ObjectSpawner spawner = garbageSpawner.GetComponent<ObjectSpawner>();
            spawner.numSpawned -= 1;
            if (health == 0)
            {
                ShowPopup("You died!");
            }
        }
        else if (collision.gameObject.CompareTag(targetTag2))
        {
            if (health < 5)
            {
                health = health + 1;
                //Debug.Log("Health: " + health);
                //Debug.Log("Index: " + (health-1));
                SpriteRenderer spriteRenderer = hearts[health - 1].GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = heartFull;
            }
            foodEaten++;
            ObjectSpawner spawner = foodSpawner.GetComponent<ObjectSpawner>();
            spawner.numSpawned -= 1;
            Destroy(collision.gameObject);
            progressBar.size += 0.1f;
            if (foodEaten > 10)
            {
                ShowPopup("You won!");
            }
        }
        else if (collision.gameObject.CompareTag(targetTag3))
        {
            moveSpeed = 2f;
            Invoke(nameof(resetSpeed), 10f);
            Destroy(collision.gameObject);
        }
    }

    private void resetSpeed()
    {
        moveSpeed = 4f;
    }

    void ShowPopup(string message)
    {
        if (popupUI != null)
        {
            _popupUIText.text = message;
            popupUI.SetActive(true);
        }
    }

    public void HidePopup()
    {
        if (popupUI != null)
        {
            popupUI.SetActive(false);
        }
    }
}
