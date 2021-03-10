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
    public float checkRadius = 0.1f;
    public LayerMask whatIsGround;

    public int extraJumps = extraJumpValue;

    public Animator animator;

    float horizontalMove = 0f;



    public int maxHp = 100;

    public HealthBar healthBar;

    public int collisionDmg = 5;

    const float colInvincibilityTimeVal = 0.75f;
    float colInvincibilityTime = 0f;

    float deathHeight = -10;

    private GameObject gameController;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Rigid body is null in start()");
        }

        sr = GetComponent<SpriteRenderer>();        
        matDefault = sr.material;

        gameController= GameObject.FindGameObjectWithTag("GameController"); 
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
            SoundManagerScript.PlaySound("jumpSound");
        }
        else if ((Input.GetButtonDown("Jump") && extraJumps > 0))
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            SoundManagerScript.PlaySound("jumpSound");
        }

        //Collision Invincibility Time
        if (colInvincibilityTime > 0)
        {
            colInvincibilityTime -= Time.deltaTime;
        }

        //checking death height
        if (transform.position.y < deathHeight)
        {
            gameController.GetComponent<GameController>().GameOver();
            Destroy(gameObject);
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

    private void OnCollisionStay2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Enemy"))
        {
         //   Debug.Log("OnCollisionStay2D");
            if (colInvincibilityTime <= 0)
            {
                receiveDmg(collisionDmg);
                colInvincibilityTime = colInvincibilityTimeVal;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      //  if (collision.gameObject.CompareTag("Enemy")) Debug.Log("OnCollisionExit2D");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       // if (col.gameObject.CompareTag("Enemy")) Debug.Log("OnCollisionEnter2D"); 
        if (col.gameObject.GetComponent<DamagingObject>() != null)
        {
            receiveDmg(col.gameObject.GetComponent<DamagingObject>().getDamage());
        }


    }

    public override void receiveDmg(float dmg)
    {
        hp -= dmg;
        healthBar.SetHealth((int)hp);
        sr.material = matRed;
        SoundManagerScript.PlaySound("playerHurtSound");
        if (hp <= 0) { 
            gameController.GetComponent<GameController>().GameOver();
            Destroy(gameObject); 
        }
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }

    public void receiveHp(float addedHp)
    {        
        if ((hp+ addedHp) > maxHp) hp = maxHp; else hp += addedHp;
        healthBar.SetHealth((int)hp);
    }


}
