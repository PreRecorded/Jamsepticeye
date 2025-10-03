using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float stepSize = 1f; // How far to move per press
    public float moveSpeed;
    public float checkObstacleRadius = 0.4f;
    public PolygonCollider2D worldBoundary;


    public LayerMask obstacleLayerMask;
    private Vector2 targetPosition;
    private bool isMoving = false;
   
    private Rigidbody2D rb;

    void Start()
    {
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        
        if (isMoving)
        {
            // Calculate direction toward the target
            Vector2 direction = (targetPosition - rb.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            // Stop when we're close enough
            if (Vector2.Distance(rb.position, targetPosition) < 0.05f)
            {
                rb.position = targetPosition;
                rb.linearVelocity = Vector2.zero;
                isMoving = false;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop when not moving
        }
    }
    // 👇 This gets called once whenever a Move input is performed
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;        // Only run on key press
        if (isMoving) return;                  // Prevent stacking moves

        Vector2 input = context.ReadValue<Vector2>();
        Vector2 moveDir = Vector2.zero;

        if (input.y > 0.5f) moveDir = Vector2.up;    // W / Up
        else if (input.y < -0.5f) moveDir = Vector2.down;  // S / Down
        else if (input.x < -0.5f) moveDir = Vector2.left;  // A / Left
        else if (input.x > 0.5f) moveDir = Vector2.right; // D / Right
        if (moveDir == Vector2.zero) return;

        
        Vector2 desiredTarget = rb.position + moveDir * stepSize;
        desiredTarget.x = Mathf.Clamp(desiredTarget.x, worldBoundary.bounds.min.x, worldBoundary.bounds.max.x);
        desiredTarget.y = Mathf.Clamp(desiredTarget.y, worldBoundary.bounds.min.y, worldBoundary.bounds.max.y);

        Collider2D hit = Physics2D.OverlapCircle(desiredTarget, checkObstacleRadius, obstacleLayerMask);
        if(hit == null)
        {
            targetPosition = desiredTarget;
            isMoving = true;
        }
        else isMoving = false;


    }
}
