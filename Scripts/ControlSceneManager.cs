using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlSceneManager : MonoBehaviour
{
    //Variables for scene management
    [SerializeField] TextMeshProUGUI controlText;
    [SerializeField] float timeBetweenControls = 10f;

    LevelManager levelManager;


    //Handles reference for dependencies
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }


    //Starts coroutine for updating control text
    void Start()
    {
        StartCoroutine(ControlManager());
    }


    //Updates text that displays control scheme
    IEnumerator ControlManager()
    {
        yield return new WaitForSeconds(3f);

        MoveControls();

        yield return new WaitForSeconds(timeBetweenControls);

        ShootControls();

        yield return new WaitForSeconds(timeBetweenControls);

        TurnControls();

        yield return new WaitForSeconds(timeBetweenControls);

        MissileControls();

        yield return new WaitForSeconds(timeBetweenControls);

        GetComfortable();

        yield return new WaitForSeconds(timeBetweenControls);

        levelManager.LoadMainMenu();
    }


    //Displays move controls
    void MoveControls()
    {
        controlText.text = "Use WASD to Move";
    }


    //Displays shooting controls
    void ShootControls()
    {
        controlText.text = "Use Spacebar or Left Mouse Button to Shoot";
    }


    //Displays turning controls
    void TurnControls()
    {
        controlText.text = "Use Left and Right Arrow Keys to Turn";
    }


    //Displays missile firing controls
    void MissileControls()
    {
        controlText.text = "Use F or Right Mouse Button to Fire Missiles";
    }


    //Alerts player of scene change
    void GetComfortable()
    {
        controlText.text = "Get comfortable with the controls!\nYou will be returned the main menu shortly.";
    }
}
