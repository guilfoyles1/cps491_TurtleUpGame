using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class myController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 7f;

    [Header("Click-to-Move Settings")]
    [SerializeField] bool allowRunOnClick = true;
    [SerializeField] float clickRunStopDistance = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 targetVelocity;
    private Vector2 lastMoveDirection = Vector2.down;
    [SerializeField] GroundChecker groundChecker;

    private Vector2? targetPosition = null;
    private bool isClickRunning = false;
    private float lastClickTime;

    [Header("Stamina Settings")]
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float staminaDrainRate = 20f;
    [SerializeField] float staminaRegenRate = 15f;
    private bool canSprint = true;


    private float currentStamina;

    [Header("Stamina Bar Sprites")]
    public List<GameObject> staminaBarImages; // Bar0 to Bar7


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleKeyboardInput();
        HandleClickInput();
        UpdateAnimations();
        UpdateStamina();
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

            float speed = walkSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && canSprint)
            {
                speed = runSpeed;
            }

            targetVelocity = input * speed;
        }

        else if (!targetPosition.HasValue)
        {
            targetVelocity = Vector2.zero;
        }
    }


    void HandleClickInput()
    {
#if UNITY_ANDROID || UNITY_IOS
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            if (IsTouchOverUI(touch.position))
                return;

            targetPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (allowRunOnClick && Time.time - lastClickTime < 0.3f)
            {
                isClickRunning = !isClickRunning;
            }

            lastClickTime = Time.time;
        }
    }
#else
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTouchOverUI(Input.mousePosition))
                return;

            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (allowRunOnClick && Time.time - lastClickTime < 0.3f)
            {
                isClickRunning = !isClickRunning;
            }

            lastClickTime = Time.time;
        }
#endif
    }



    void FixedUpdate()
    {
        if (targetPosition.HasValue)
        {
            Vector2 direction = (targetPosition.Value - (Vector2)transform.position);
            float distance = direction.magnitude;

            if (distance > clickRunStopDistance)
            {
                float speed = (isClickRunning && canSprint) ? runSpeed : walkSpeed;
                targetVelocity = direction.normalized * speed;
            }

            else
            {
                targetVelocity = Vector2.zero;
                targetPosition = null;
            }
        }

        rb.velocity = targetVelocity;
    }


    void UpdateAnimations()
    {
        Vector2 velocity = rb.velocity;
        bool isMoving = velocity.magnitude > 0.1f;
        bool isRunning = (Input.GetKey(KeyCode.LeftShift) || isClickRunning) && canSprint && isMoving;


        if (isMoving)
        {
            lastMoveDirection = velocity.normalized;
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

    private bool IsTouchOverUI(Vector2 screenPos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPos;

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    void UpdateStamina()
    {
        bool isTryingToRun = (Input.GetKey(KeyCode.LeftShift) || isClickRunning) && canSprint && targetVelocity.magnitude > 0.1f;

        if (isTryingToRun)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);

            // Once stamina runs out, stop sprinting
            if (currentStamina <= 0f)
            {
                canSprint = false;
            }
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);

            // Re-enable sprinting only when fully recharged
            if (currentStamina >= maxStamina)
            {
                canSprint = true;
            }
        }

        UpdateStaminaBarVisual();
    }



    void UpdateStaminaBarVisual()
    {
        int index = Mathf.RoundToInt((currentStamina / maxStamina) * (staminaBarImages.Count - 1));

        for (int i = 0; i < staminaBarImages.Count; i++)
        {
            staminaBarImages[i].SetActive(i == index);
        }
    }


}
