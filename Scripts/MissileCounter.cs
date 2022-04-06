using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissileCounter : MonoBehaviour
{
    //Variables for tracking missile count
    PickupHandler pickupHandler;
    TextMeshProUGUI missileText;


    //Handles references for dependencies
    void Awake()
    {
        pickupHandler = FindObjectOfType<PickupHandler>();
        missileText = gameObject.GetComponent<TextMeshProUGUI>();
    }


    //Initializes UI missile counter and missile value
    void Start()
    {
        int missileCount = Mathf.Clamp(pickupHandler.GetMissileCount(), 0, 99);
        missileText.text = missileCount.ToString("00");
    }


    //Udpates UI missile counter and missile value
    public void UpdateMissileCounter()
    {
        int missileCount = Mathf.Clamp(pickupHandler.GetMissileCount(), 0, 99);
        missileText.text = missileCount.ToString("00");
    }
}
