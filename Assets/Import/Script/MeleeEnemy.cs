using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    
    private float cooldownTimer = Mathf.Infinity;
    
    private Animator anim;
    private HealthSystem playerHealth;
    private EnemyPatrol enemyPatrol;

   
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerinSight())
        {
            
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
               //DamagePlayer();
                //GetComponent<EnemyPatrol>().speed *= 4;
    
            }
        }

        //if (enemyPatrol != null)
        {
        // enemyPatrol.enabled = !PlayerinSight();
        }

      
    }

    public bool PlayerinSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if (hit.collider !=null)
            playerHealth = hit.transform.GetComponent<HealthSystem>();
           
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerinSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

  

}