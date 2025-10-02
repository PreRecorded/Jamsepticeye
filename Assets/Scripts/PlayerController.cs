using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float rotationSpeed = 10f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = moveValue * moveSpeed;

        if (moveValue.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(moveValue.y, moveValue.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }

    }
}
