using System.Collections; 
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 
    public Text scoreText; 
    public Text highscoreText;

    int score = 0; 
    int highscore = 0; 
     
    private void Awake()
    {
        instance = this; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = score.ToString() + " POINTS";
        highscoreText.text = " HIGHSCORE:" + highscore.ToString();
    } 

  public void AddPoint(int pointValue)
    {
        score = score + pointValue;
        scoreText.text = score.ToString();
    }
}