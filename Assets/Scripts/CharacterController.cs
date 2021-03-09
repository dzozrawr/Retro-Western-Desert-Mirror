using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : HealthObject
{
    private const int extraJumpValue = 1;

    public float charSpeed = 230f;
    public float jumpForce = 6f;


    private bool facingRight = true;
    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.01f;
    public LayerMask whatIsGround;

    public int extraJumps = extraJumpValue;

    public Animator animator;

    float horizontalMove = 0f;
    

    private Material matRed;
    private Material matDefault;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        hp = 20;
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
        matRed = Resources.Load("RedFlash", typeof(Material)) as Material;
        matDefault = sr.material;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * charSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
            if (rb.velocity.y <= 0) animator.SetBool("isJumping", false);
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

        if (facingRight == false && Input.GetAxisRaw("Horizontal") > 0)
        {
            Flip();
        }
        else if (facingRight == true && Input.GetAxisRaw("Horizontal") < 0)
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<DamagingObject>() != null)
        {
            receiveDmg(col.gameObject.GetComponent<DamagingObject>().getDamage());
        }
    }

    public override void receiveDmg(float dmg)
    {
        hp -= dmg;
        sr.material = matRed;
        if (hp <= 0) Destroy(gameObject);
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }
}
