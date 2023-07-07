using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;         // Player movement speed
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float jumpForce;         // Initial force applied when jumping
    public float maxJumpTime = 0.5f;     // Maximum time the player can hold the jump button
    public float jumpTimeMultiplier;// Multiplier to increase jump force over time
    public Transform groundCheck;        // Object used to check if the player is touching the ground
    public LayerMask groundLayer;        // Layer that represents the ground

    private Rigidbody2D rb;              // Player's rigidbody component
    private bool isJumping = false;      // Flag to track if the player is jumping
    private bool isGrounded = false;     // Flag to track if the player is grounded
    private float jumpTime = 0f;         // Time the jump button has been held

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = walkSpeed;
    }

    private void Update()
    {
        // Check if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Player movement
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Player jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            jumpTime = 0f;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTime < maxJumpTime)
            {
                rb.AddForce(new Vector2(0f, jumpForce * jumpTimeMultiplier), ForceMode2D.Impulse);
                jumpTime += Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // Perform actions when the left shift button is released
            moveSpeed = walkSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump flag when the player lands on a surface
        if (collision.gameObject.layer == groundLayer)
        {
            isJumping = false;
        }
    }
}