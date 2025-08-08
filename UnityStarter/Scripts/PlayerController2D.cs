using UnityEngine;

/// <summary>
/// Simple 2D platformer controller with horizontal movement and jump.
/// Requires: Rigidbody2D and a Collider2D on the same GameObject.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeedUnitsPerSecond = 8f;
    [SerializeField] private float airControlMultiplier = 0.8f;

    [Header("Jump")]
    [SerializeField] private float jumpImpulse = 14f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private int extraAirJumps = 0; // 0 = only ground jump

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer spriteRenderer; // optional; if null, will try to find one

    private Rigidbody2D cachedRigidbody2D;
    private int remainingAirJumps;
    private float inputHorizontalRaw;

    private bool IsGrounded
    {
        get
        {
            if (groundCheck == null)
            {
                return false;
            }

            Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
            return hit != null;
        }
    }

    private void Awake()
    {
        cachedRigidbody2D = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void OnEnable()
    {
        ResetAirJumps();
    }

    private void Update()
    {
        inputHorizontalRaw = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }

        HandleVisualFlip();
    }

    private void FixedUpdate()
    {
        float control = IsGrounded ? 1f : Mathf.Clamp01(airControlMultiplier);
        float targetVelocityX = inputHorizontalRaw * moveSpeedUnitsPerSecond * control;

        Vector2 velocity = cachedRigidbody2D.velocity;
        velocity.x = targetVelocityX;
        cachedRigidbody2D.velocity = velocity;

        if (IsGrounded && Mathf.Abs(cachedRigidbody2D.velocity.y) < 0.01f)
        {
            ResetAirJumps();
        }
    }

    private void TryJump()
    {
        if (IsGrounded)
        {
            PerformJump();
            ResetAirJumps();
            return;
        }

        if (remainingAirJumps > 0)
        {
            PerformJump();
            remainingAirJumps--;
        }
    }

    private void PerformJump()
    {
        Vector2 velocity = cachedRigidbody2D.velocity;
        velocity.y = 0f; // reset vertical speed before jump for consistent height
        cachedRigidbody2D.velocity = velocity;
        cachedRigidbody2D.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
    }

    private void ResetAirJumps()
    {
        remainingAirJumps = Mathf.Max(0, extraAirJumps);
    }

    private void HandleVisualFlip()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (inputHorizontalRaw > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputHorizontalRaw < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}