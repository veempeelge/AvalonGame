using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public float totalHealth;
    public Animator anim;
    public Rigidbody2D rb;
    public Collider2D col;
   
    [SerializeField] private float iFrameDuration;
    [SerializeField] private uint numberOfFlashes;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private Behaviour[] components;

    [SerializeField] private GameObject bossWall;
    public float currentHealth
    {
        get;
        private set;
    }

    private void Awake()
    {
        currentHealth = totalHealth;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - _amount, 0, totalHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(Iframe());
          
        }
        else if (currentHealth <= 0)
        {

            Debug.Log("Game Over");
            foreach (Behaviour component in components)
                component.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            //Invoke("LoadScene", 2);
            ScoreSystem.scoreValue -= 50;
            //playerdead
            col.enabled = false;
            bossWall.SetActive(false);
        }
    }

    private void Update()
    {
     
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

 

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, totalHealth);
    }

    private IEnumerator Iframe()
    {
        Debug.Log("StartIFrame");
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void RespawnPlayer()
    {
        AddHealth(totalHealth);
        anim.ResetTrigger("PlayerDead");
        anim.Play("PlayerIdle");
        StartCoroutine(Iframe());
        foreach (Behaviour component in components)
            component.enabled = true;
        anim.SetBool("Dead", false);
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = true;
        SoundManager.instance.PlayRespawn();
    }
}
