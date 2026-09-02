using System.Collections; 
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 
    public Text ScoreText; 
    public Text HighscoreText;

    int Score = 0; 
    int Highscore = 0; 
     
    private void Awake()
    {
        instance = this; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Highscore = PlayerPrefs.GetInt("Highscore", 0);
        ScoreText.text =  "Score: " + Score.ToString() ;
        HighscoreText.text = "Highscore: " + Highscore.ToString();
    } 

  public void AddPoint(int pointValue)
    {
        Score = Score + pointValue;
        ScoreText.text = "Score: " + Score.ToString();
        if (Score > Highscore)
        {
            PlayerPrefs.SetInt("Highscore", Score);
        }
        
    }
}