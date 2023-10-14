using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    float horizontalMove = 0;
    float Speed;
    float health;
    public Animator anim;
    private bool isGrounded;
    public bool isWallSliding;
    public bool isWallJumping;
    public bool isFalling;
    public bool isPlayingSound;
    [SerializeField] private AudioClip[] walkingSound;
    private int randomWalking;

    
    [SerializeField] private AudioClip jumpingSound;
    [SerializeField] private AudioClip fallingSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isWallSliding = GetComponent<PlayerMovement>().isWallSliding;
        isWallJumping = GetComponent<PlayerMovement>().isWallJumping;
        isFalling = GetComponent<PlayerMovement>().isFalling();
        isGrounded = GetComponent<PlayerMovement>().IsGrounded();
        Speed = GetComponent<PlayerMovement>().speed;
        health = GetComponent<HealthSystem>().currentHealth;
        horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        isPlayingSound = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Jumping", true);

            SoundManager.instance.PlayJump();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Jumping", false);
      

        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Dashing", false);
        }

        if (health <= 0)
        {
            anim.SetBool("Dead", true);
        }

        if (isGrounded == true)
        {
            anim.SetBool("Grounded", true);
            anim.SetBool("Falling", false);
        }
        else
        {
            anim.SetBool("Grounded", false);
          
            
        }

        if (isFalling == true)
        {
           
            anim.SetBool("Falling", true);
         
        }
        else
        {
            anim.SetBool("Falling", false);
           
        }
    }
}
