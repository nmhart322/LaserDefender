using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    //Variables for keeping score
    [SerializeField] TextMeshProUGUI scoreText;

    int currentScore = 0;

    static ScoreKeeper instance;


    //Manages singleton and handles references for dependencies
    void Awake()
    {
        ManageSingleton();

        if(scoreText == null)
        {
            scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        }
    }


    //Accessor for current score
    public int GetScore()
    {
        return currentScore;
    }


    //Updates score
    public void ModifyScore(int score)
    {
        currentScore += score;
        Mathf.Clamp(score, 0, int.MaxValue);
        scoreText.text = currentScore.ToString("000000000");
    }


    //Resets score on scene load
    public void ResetScore()
    {
        currentScore = 0;
    }


    //Manages singleton on scene load
    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
