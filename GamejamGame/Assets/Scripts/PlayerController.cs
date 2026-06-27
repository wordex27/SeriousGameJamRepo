using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Look Settings")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float minLookAngle = -80f;
    [SerializeField] private float maxLookAngle = 80f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private float verticalRotation = 0f;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        // Lock and hide cursor for seamless FPS feel
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Clean up Rigidbody settings required for responsive controllers
        rb.freezeRotation = true;
        rb.useGravity = true;
        
        // High collision detection prevents clipping through walls during frantic gameplay
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; 
    }

    private void Update()
    {
        HandleLook();
        CheckGroundStatus();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleLook()
    {
        if (playerCamera == null) return;

        // Mouse inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // Rotate Player Body left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate Camera up/down (clamped)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minLookAngle, maxLookAngle);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        // Get WASD / Arrow Key inputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Calculate direction relative to where the player is looking
        Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX).normalized;

        // Determine current target speed
        float currentSpeed = Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed;

        if (isGrounded)
        {
            // Directly alter X and Z velocities while preserving gravity's Y pull
            Vector3 targetVelocity = moveDirection * currentSpeed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        }
        else
        {
            // Optional: Reduce control slightly in mid-air if dropped from a ledge
            Vector3 airVelocity = moveDirection * currentSpeed * 0.75f;
            rb.linearVelocity = new Vector3(airVelocity.x, rb.linearVelocity.y, airVelocity.z);
        }
    }

    private void CheckGroundStatus()
    {
        // Calculate the bottom point of the capsule
        Vector3 capsuleBottom = transform.position + Vector3.up * (capsule.radius - groundCheckDistance);
        
        // Cast a small sphere down to see if it hits the designated ground layers
        isGrounded = Physics.CheckSphere(capsuleBottom, capsule.radius, groundLayer, QueryTriggerInteraction.Ignore);
    }
}