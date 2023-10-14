using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{

    [SerializeField] public float enemyHealth;
    [SerializeField] private float enemyTotalHealth;



    [SerializeField] private float iFrameDuration;
    [SerializeField] private uint numberOfFlashes;
    [SerializeField] private SpriteRenderer spriteRend;
   // [SerializeField] private Behaviour[] components;
    [SerializeField] private Animator anim;

    [SerializeField] int enemyScore;
    private ScoreSystem scoreValue;
    [SerializeField] Collider2D boxcollider2d;

    private EnemyPatrol ep;
    public GameObject bossWall;
    [SerializeField] GameObject wallTrigger;
    private 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void TakeDamage(float _amount)
    {
        enemyHealth = Mathf.Clamp(enemyHealth - _amount, 0, enemyTotalHealth);

        if (enemyHealth > 0)
        {
            StartCoroutine(Iframe());
            Debug.Log("Enemy health = " + enemyHealth);
        }
        else if (enemyHealth <= 0)
        {
            Debug.Log("Enemy ded + " + enemyScore);

            //Invoke("LoadScene", 2);
            anim.SetTrigger("die");
            //playerdead
            //scoreValue.addScore(enemyScore);
            ScoreSystem.scoreValue += enemyScore;
            boxcollider2d.enabled = false;
            bossWall.SetActive(false);
            wallTrigger.SetActive(false);
          
        }
    }
    private IEnumerator Iframe()
    {
        Debug.Log("EnemyIframe");
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRend.color = new Color(1, 0, 0, 1f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void destroyenemy()
    {
        Destroy(gameObject);
    }

}
