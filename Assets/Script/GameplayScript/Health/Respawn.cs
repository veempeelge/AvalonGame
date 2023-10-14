
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private HealthSystem playerHealth;
    public Rigidbody2D rb;
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        playerHealth = GetComponent<HealthSystem>();
      
    }

    public void RespawnPlayerr()
    {
        transform.position = currentCheckpoint.position;
        playerHealth.RespawnPlayer();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            //collision.GetComponent<Collider2D>().enabled = false;
            Debug.Log("checkpoint set to " + collision.name);
            //collision.GetComponent<Animator>().SetTrigger("activate");
            
        }
    }
}

