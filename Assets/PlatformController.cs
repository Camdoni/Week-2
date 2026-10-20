using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private float moveX;

    [SerializeField] float jumpForce;
    [SerializeField] int maxJumps = 2;
    public bool isTouchingGround;
    int jumpsLeft;
    public int coinsCollected = 0;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    bool isMovingRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isJumping", !isTouchingGround);
        animator.SetBool("isRunning", !Mathf.Approximately(moveX, 0f));
        if (isMovingRight && moveX < 0)
        {

        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = moveX * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveX = value.Get<Vector2>().x;
    }

    void OnJump(InputValue value)
    {

        if (value.isPressed && jumpsLeft > 0)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            jumpsLeft--;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isTouchingGround = true;
            jumpsLeft = maxJumps;
        }

        if (collision.collider.CompareTag("Hazard"))
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(rb);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isTouchingGround = false;
        }
    }
}
