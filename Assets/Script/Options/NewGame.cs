using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class NewGame : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreValue;
    // Start is called before the first frame update
    void Start()
    {



    }



    // Update is called once per frame
    void Update()
    {



    }



    public void ChangeScene()
    {
        SceneManager.LoadScene("Lv One");
        ScoreSystem.scoreValue = 0;
    }



}