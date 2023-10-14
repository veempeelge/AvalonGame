using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    public float TotalHealth;



    private SpriteRenderer spriteRend;
    // Start is called before the first frame update
    void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / TotalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / TotalHealth;
    }


}
