using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float stepSize = 1f; // How far to move per press
    public float moveSpeed;
    public float checkObstacleRadius = 0.4f;
    public PolygonCollider2D worldBoundary;
    public float rotationSpeed = 10f;
    AudioSource soundFX_Source;
    public AudioClip Walking_Sound_01;
    public LayerMask obstacleLayerMask;
    private Vector2 targetPosition;
    private bool isMoving = false;

    private Animator anim;

    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        soundFX_Source = GameObject.FindGameObjectWithTag("SoundFX").GetComponent<AudioSource>();
    }
    private void Update()
    {
        anim.SetBool("isWalking", isMoving);
    }
    void FixedUpdate()
    {
        
        if (isMoving)
        {
            // Calculate direction toward the target
            Vector2 direction = (targetPosition - rb.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

/*            if (direction.sqrMagnitude > 0.01f)
                RotateTowards(direction);*/

            // Stop when we're close enough
            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
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
        else if (input.x < -0.5f)
        {
            moveDir = Vector2.left;  // A / Left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (input.x > 0.5f)
        {
            moveDir = Vector2.right; // D / Right
            transform.localScale = Vector3.one;
        }
        if (moveDir == Vector2.zero) return;


        Vector2 desiredTarget = rb.position + moveDir * stepSize;
        desiredTarget.x = Mathf.Clamp(desiredTarget.x, worldBoundary.bounds.min.x, worldBoundary.bounds.max.x);
        desiredTarget.y = Mathf.Clamp(desiredTarget.y, worldBoundary.bounds.min.y, worldBoundary.bounds.max.y);

        Collider2D hit = Physics2D.OverlapCircle(desiredTarget, checkObstacleRadius, obstacleLayerMask);
        if (hit == null)
        {
            targetPosition = desiredTarget;
            soundFX_Source.PlayOneShot(Walking_Sound_01);
            //RotateTowards(moveDir);
            isMoving = true;
        }
        else isMoving = false;


        }

    private void RotateTowards(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle); // -90f if your sprite points up by default

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
