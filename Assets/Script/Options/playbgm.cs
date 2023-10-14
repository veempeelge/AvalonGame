using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playbgm : MonoBehaviour
{

    [SerializeField] GameObject bgm;
    [SerializeField] AudioSource bgmm;
    // Start is called before the first frame update
    void Start()
    {
      //  bgm.SetActive(true);
        bgmm.enabled = true;
    }
    private void Awake()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
