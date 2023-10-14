using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioClip jump, fall, dash, respawn, attack, attack2, attackhit, attackhit2, portal;
    [SerializeField] private AudioSource soundFX, bgMusic, atk;
    public AudioSource footstep;
    public PlayerMovement playerMovement;
    private bool isGrounded;
    private bool isPlayingSound;
    public Rigidbody2D rbbb;
    private void Update()

    {
        isGrounded = playerMovement.IsGrounded();
        if ( rbbb.velocity.x != 0 && isGrounded)
        {
            footstep.enabled = true;
        }
        else
        {
            footstep.enabled = false;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
       
    }

    public void PlayFall()
    {
      soundFX.PlayOneShot(fall);
    }

    public void PlayJump()
    {
       soundFX.PlayOneShot(jump);
    }

    public void PlayDash()
    {
        soundFX.PlayOneShot(dash);
    }

    public void PlayRespawn()
    {
        soundFX.PlayOneShot(respawn);
    }
    public void PlayAttack()
    {
        atk.PlayOneShot(attack, .3f);
    }
    public void PlayAttack2()
    {
        atk.PlayOneShot(attack2, .3f);
    }
    public void PlayAttackHit()
    {
       atk.PlayOneShot(attackhit, .3f);
    }
    public void PlayAttackHit2()
    {
       atk.PlayOneShot(attackhit2, .3f);
    }
    public void PlayTeleport()
    {
        soundFX.PlayOneShot(portal);
    }
}
