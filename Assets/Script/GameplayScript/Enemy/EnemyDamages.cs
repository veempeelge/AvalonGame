using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamages : MonoBehaviour
{
    

    [SerializeField] private float Damage;
    // Start is called before the first frame update


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.GetComponent<HealthSystem>().TakeDamage(Damage);
        }
    }
}
