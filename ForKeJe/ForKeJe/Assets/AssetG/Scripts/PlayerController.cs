using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Movement")]
    public float speed;
    private float moveInput;

    [Header("Jumping")]
    private bool isGrounded;
    public float jumpForce;
    public bool doubleOffLadder;
    private int extraJumps;
    public int extraJumpValue;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float checkRadius;

    [Header("Climbing")]
    public LayerMask whatIsLadder;
    private bool isClimbing, isLaddered;
    private float inputVertical;
    public float climbingSpeed;
    public float distance;

    [Header("Health")]
    public float maxhealth = 20;
    [HideInInspector]
    public float health;
    public GameObject deatheffect;

    [Header("Lives & Spawning")]
    public int startingLives = 3;
    [HideInInspector]
    public int lives;
    public GameObject spawnPoint;
    public GameObject player;

    [Header("Physics")]
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Animation")]
    public bool facingRight = true;

    [Header("Attacking")]
    public float goombaDmg = 10f;

    #endregion

    private void Start()
    {
        lives = startingLives;
        extraJumps = extraJumpValue;

        rb = GetComponent<Rigidbody2D>();
    }

    #region KeyChecking
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (doubleOffLadder)
        {
            isLaddered = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsLadder);
        }
        else
        {
            isLaddered = false;
        }

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * climbingSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
    #endregion

    #region Movement Application
    private void Update()
    {
        if (isGrounded || isLaddered)
        {
            extraJumps = extraJumpValue;
        }

        if (((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W))) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W))) && extraJumps == 0 && (isGrounded || isLaddered))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    #endregion

    #region Animation
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    #endregion
}