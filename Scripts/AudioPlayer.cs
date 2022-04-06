using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    //Variables for Shooting Audio
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;
    [SerializeField] AudioClip missileFireClip;
    [SerializeField] [Range(0f, 1f)] float missileVolume = 1f;

    //Variables for Damage Audio
    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] List<AudioClip> damageClips;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    static AudioPlayer instance;


    //Handles audio player singleton
    void Awake()
    {
        ManageSingleton();
    }


    //Calls PlayClip() and passes audio clip and audio volume for shooting
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }


    //Calls PlayClip() and passes audio clip and audio volume for damage
    public void PlayDamageClip()
    {
        int index = (int)Random.Range(0, damageClips.Count - 1);

        PlayClip(damageClips[index], damageVolume);
    }


    //Calls PlayClip() and passes audio clip and audio volume for missile firing
    public void PlayMissileClip()
    {
        PlayClip(missileFireClip, missileVolume);
    }


    //Plays audio clip with volume at camera position
    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }


    //Handles singleton management, ensures audio player is persistent across scene loading
    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);                                    //if audio player exists in scene, destroy this
            Destroy(gameObject);
        }

        else
        {
            instance = this;                                                //if audio player does not exist in scene, make this persistent
            DontDestroyOnLoad(gameObject);
        }
    }
}
