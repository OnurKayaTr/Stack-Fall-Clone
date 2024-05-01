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
        scoreTXT = GameObject.Find("T1").GetComponent<Text>();
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
        if (scoreTXT == null)
        {
            scoreTXT = GameObject.Find("T1").GetComponent<Text>();
        }
}
    public void addScore(int value )
    {
        score += value;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreTXT.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
            
    }
}
