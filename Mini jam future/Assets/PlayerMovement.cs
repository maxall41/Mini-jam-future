using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    public bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public AudioSource jump;

    private float JumpPressedRemember;

    public float JumpRememberTime;

    private float InternalCooldown = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        } else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (moveInput > 0)
        {
            gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else if (moveInput < 0)
        {
            gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsWalking", false);
        }
        InternalCooldown -= Time.deltaTime;
        JumpPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPressedRemember = JumpRememberTime;
        }
        if ((JumpPressedRemember > 0) && isGrounded == true)
        {
            JumpPressedRemember = 0;
            rb.velocity = Vector2.up * jumpForce;
            jump.Play();
        } 
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        GameObject.Find("GlitchVac").GetComponent<GlitchVac>().FlipGlitchVac();
    }

    void RestartLevel()
    {
        //TODO: REPLACE THIS CODE TO WORK WITH THE NEW LEVEL LOADER
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
