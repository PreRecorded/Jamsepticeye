using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;


    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.right * moveSpeed;
    }


}
