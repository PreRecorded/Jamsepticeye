using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float stepSize = 1f;
    Rigidbody2D rb;
    Vector2 targetPosition;

    bool isMoving = false;  
    private void Start()
    {
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        if (!isMoving && targetPosition != Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.W))
                targetPosition += Vector2.up * stepSize; // Forward (+Z)
            if (Input.GetKeyDown(KeyCode.S))
                targetPosition += Vector2.down * stepSize;    // Backward (-Z)
            if (Input.GetKeyDown(KeyCode.A))
                targetPosition += Vector2.left * stepSize;    // Left (-X)
            if (Input.GetKeyDown(KeyCode.D))
                targetPosition += Vector2.right * stepSize;   // Right (+X)
            transform.position = targetPosition;
        }

        
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        // rb.linearVelocity = moveValue * moveSpeed;
       // Vector3 targetPosition = transform.position + (Vector3)moveValue;
      //  transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0f);
        if (moveValue.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(moveValue.y, moveValue.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }

    }


    private void FixedUpdate()
    {
        if (isMoving)
        {
            // Smooth physics movement toward the target
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));

            // Stop when close enough
            if (Vector2.Distance(rb.position, targetPosition) < 0.001f)
            {
                rb.MovePosition(targetPosition);
                isMoving = false;
            }
        }
    }
}
