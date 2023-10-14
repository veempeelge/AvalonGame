using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    float horizontalMove = 0;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int atkdamage;
    
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask enemyLayer;
    
    private float cooldownTimer = 0;
    private float cooldownTimer2 = 0;
    private float comboTimeLimit = 2;

    private Animator anim;
    private EnemyHealthSystem enemyHealth;
    private EnemyPatrol enemyPatrol;
    private float speed = 8f;
    public bool canattack;
    public bool isattacking = false;
    bool doubleattack;

    private float attackcombo;
    [SerializeField] private Behaviour[] component;
    public Rigidbody2D rbb;
    private bool isGrounded;

    private void Awake()
    {
        attackcombo = 1;
        canattack = true;
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        speed = GetComponent<PlayerMovement>().speed;
        isGrounded = GetComponent<PlayerMovement>().IsGrounded();
        cooldownTimer += Time.deltaTime;
        cooldownTimer2 += Time.deltaTime;

        if (cooldownTimer2 > comboTimeLimit)
        {
            attackcombo = 1;
           
        }




        //Debug.Log(cooldownTimer);

      

        

        //if (enemyPatrol != null)
        {
        // enemyPatrol.enabled = !PlayerinSight();
        }
        DamageEnemy();
    }

    public bool enemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, enemyLayer);

        if (hit.collider !=null)
            enemyHealth = hit.transform.GetComponent<EnemyHealthSystem>();
           
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamageEnemy()
    {


        if (Input.GetMouseButtonDown(0) && rbb.velocity.x == 0 && isGrounded) 
        {
            Debug.Log("atk combo " + attackcombo);
            if (attackcombo == 1  && canattack)
            {
                anim.SetBool("DoubleAttack", false);
                anim.SetBool("Attack", true);
                cooldownTimer2 = 0;
                attackcombo = 2;
                canattack = false;
                
            }

            else if (attackcombo == 2)
            {
                anim.SetBool("DoubleAttack",true);
                anim.SetBool("Attack", false);
                attackcombo = 1;
                canattack = false;
              
            }
            if (enemyInSight())
            {
                enemyHealth.TakeDamage(atkdamage);
            }

            foreach (Behaviour component in component)
                component.enabled = false;
            rbb.bodyType = RigidbodyType2D.Static;
        }

       

    }



   void AttackEnd1()
    {
        anim.SetBool("Attack", false);
        canattack = true;
        attackcombo = 2;
        foreach (Behaviour component in component)
            component.enabled = true;
        rbb.bodyType = RigidbodyType2D.Dynamic;
    }
    void AttackEnd2()
    {
        anim.SetBool("DoubleAttack", false) ;
        canattack = true;
        attackcombo = 1;
        foreach (Behaviour component in component)
           component.enabled = true;
        rbb.bodyType = RigidbodyType2D.Dynamic;


    }

    void AttackSound()
    {
        if (enemyInSight())
        {
            SoundManager.instance.PlayAttackHit();
        }
        else
        {
            SoundManager.instance.PlayAttack();
        }

    }
    void AttackSound2()
    {
        if (enemyInSight())
        {
            SoundManager.instance.PlayAttackHit2();
        }
        else
        {
            SoundManager.instance.PlayAttack2();
        }

    }

    private void DoubleAttack()
    {
       // if ((Input.GetMouseButtonDown(0) && Mathf.Abs(horizontalMove) < .1f))
        {
            //Debug.Log("DoubleAttack");
           // anim.SetBool("DoubleAttack", true);
            //if (enemyInSight())
            {
                //enemyHealth.TakeDamage(atkdamage);
               // isattacking = true;
               // canattack = false;
            }
        }

    }
}