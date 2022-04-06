using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    //Variables for updating score on game over
    [SerializeField] TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;


    //Handles references for dependencies
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    //Updates score on game over screen
    void Start()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
