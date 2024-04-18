using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score;
    public Text scoreTXT;

    private void Awake()
    {
        makesingolton();
        
    }

    private void makesingolton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }
    void Start()
    {
        addScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addScore(int value )
    {
        score += value;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void ResetScore()
    {
        score = 0;
            
    }
}
