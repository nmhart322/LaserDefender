using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Variables for level management
    [SerializeField] float sceneDelay = 2f;
    ScoreKeeper scoreKeeper;


    //Handles reference for dependencies
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    //Handles loading of game scene and resets score
    public void LoadGame()
    {
        if (scoreKeeper != null)
        {
            scoreKeeper.ResetScore();
            Destroy(scoreKeeper.gameObject);
        }

        SceneManager.LoadScene("Game");
    }


    //Handles loading of main menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


    //Handles loading of game over scene
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Game Over Menu", sceneDelay));
    }


    //Handles loading of main menu scene on quit game
    public void QuitGame()
    {
        Debug.Log("Quitting");
        SceneManager.LoadScene("Main Menu");
    }


    //Handles loading of controls scene
    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }


    //Coroutine for loading scene after delay
    IEnumerator WaitAndLoad(string scene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}
