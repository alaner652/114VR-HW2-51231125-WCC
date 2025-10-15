using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float rotationSpeed = 10f;

    [Header("Optional: Mouse Look")]
    public bool useMouseLook = false;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private float verticalVelocity = 0f;
    private float gravity = -9.81f;

    void Start()
    {
        // Get or add CharacterController component
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }

        // Lock cursor if using mouse look
        if (useMouseLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        HandleMovement();

        if (useMouseLook)
        {
            HandleMouseLook();
        }
    }

    void HandleMovement()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrows

        // Calculate movement direction
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection.Normalize();

        // Check if running (Left Shift)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        // Apply movement
        Vector3 move = moveDirection * currentSpeed;

        // Apply gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -2f; // Small value to keep grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        // Move the character
        controller.Move(move * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Toggle cursor lock with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ?
                CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
