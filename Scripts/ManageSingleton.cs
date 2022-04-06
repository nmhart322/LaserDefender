using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSingleton : MonoBehaviour
{
    //Holds reference to singleton instance
    static ManageSingleton instance;

    void Awake()
    {
        if (instance != null)                               //if object exists in scene, destroy this
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else
        {
            instance = this;                                // if object does not exist in scene, make this persistent
            DontDestroyOnLoad(gameObject);
        }
    }
}
