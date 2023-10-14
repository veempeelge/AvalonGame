using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDash;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    private float dashCooldown = 1f;

    public float wallSlidingSpeed;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    public bool isWallJumping;

    public bool isWallSliding;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    public TrailRenderer tr;
    public Animator anim;
  

    void Update()
    {
        if (isDash)
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
         
        }
        

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            anim.SetTrigger("isDashing");
            anim.SetBool("Dashing", true);
            SoundManager.instance.PlayDash();
        }

       


        WallSlide();

        WallJumping();
     
        Flip();

        if (isWallJumping)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        if (isFalling())
        {
            Debug.Log("fallSOund");
            //SoundManager.instance.PlayFall();
           
        }

        
       
    }


    void WallJumping()
    {
        
        if (IsWalled() && (Input.GetButtonDown("Jump")))
        {
            isWallJumping = true;
            Invoke("StopWallJump", wallJumpDuration);
            anim.SetBool("WallJump", true);
            SoundManager.instance.PlayJump();
        }
    }

    void StopWallJump()
    {
        isWallJumping = false;
        anim.SetBool("WallJump", false);
    }

    private void FixedUpdate()
    {

        if (isDash) { return; }
        rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);

        if (isWallJumping)
        {
            rb.velocity = new Vector2(-horizontal * wallJumpForce.x, wallJumpForce.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
        }
    }

    public bool isFalling()
    {
       return  rb.velocity.y < -0f && !IsGrounded();
    }
    public bool IsGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }



    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            anim.SetBool("Wallsliding", true);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed * Time.deltaTime, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
            anim.SetBool("Wallsliding", false);
        }
    }

    

   

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
           isFacingRight = !isFacingRight;
           Vector3 localScale = transform.localScale;
           localScale.x *= -1f;
           transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDash = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        anim.SetBool("Dashing", false);
    }
}
