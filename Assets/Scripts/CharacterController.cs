using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private const int extraJumpValue = 1;

    public float charSpeed = 230f;
    public float jumpForce= 7f;
    

    private bool facingRight = true;
    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius=0.01f;
    public LayerMask whatIsGround;

    public int extraJumps= extraJumpValue;

    public Animator animator;

    float horizontalMove = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * charSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
            if(rb.velocity.y<=0) animator.SetBool("isJumping", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("isJumping", true);
        }
        else if ((Input.GetButtonDown("Jump") && extraJumps > 0))
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        
        rb.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);

        if(facingRight==false && Input.GetAxisRaw("Horizontal") > 0)
        {
            Flip();
        }
        else if(facingRight==true && Input.GetAxisRaw("Horizontal") < 0)
        {
            Flip();
        }
    }
    // Update is called once per frame


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
