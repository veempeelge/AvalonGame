using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstep : MonoBehaviour
{

    [SerializeField] AudioSource footsteps;
    public PlayerMovement playerMovement;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            isGrounded = playerMovement.IsGrounded();
            if (Input.GetKey(KeyCode.A) && isGrounded || Input.GetKey(KeyCode.D) && isGrounded)
            {
                footsteps.enabled = true;
            }
            else
            {
                footsteps.enabled = false;
            }
        }
    }
}
