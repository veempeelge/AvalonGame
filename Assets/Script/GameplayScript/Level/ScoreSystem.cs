using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public static int scoreValue = 0;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score = " + scoreValue;
        if (scoreValue < 0)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void addScore(int _amount)
    {
       // scoreValue = scoreValue + _amount;
        //Debug.Log("AddingScore");
    }
}
