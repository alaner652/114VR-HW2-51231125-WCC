using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    [Tooltip("地面判定所需的標籤名稱")]
    public string groundTag = "Ground";

    private enum PlayerState
    {
        Idle,
        Walk,
        Jump
    }

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerState currentState = PlayerState.Idle;
    private float horizontalInput;
    private bool isGrounded;
    private Vector3 initialScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        initialScale = transform.localScale;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        UpdateState();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (horizontalInput > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
        SetState(PlayerState.Jump);
    }

    void UpdateState()
    {
        if (!isGrounded)
        {
            SetState(PlayerState.Jump);
            return;
        }

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            SetState(PlayerState.Walk);
        }
        else
        {
            SetState(PlayerState.Idle);
        }
    }

    void SetState(PlayerState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        switch (currentState)
        {
            case PlayerState.Idle:
                animator.SetTrigger("isIdle");
                break;
            case PlayerState.Walk:
                animator.SetTrigger("isWalk");
                break;
            case PlayerState.Jump:
                animator.SetTrigger("isJump");
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(groundTag))
        {
            isGrounded = true;
            UpdateState();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(groundTag))
        {
            isGrounded = false;
            SetState(PlayerState.Jump);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(groundTag))
        {
            isGrounded = true;
            UpdateState();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(groundTag))
        {
            isGrounded = false;
            SetState(PlayerState.Jump);
        }
    }
}
