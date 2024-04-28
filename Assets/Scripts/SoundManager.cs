using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;
    private AudioSource audioSource;
    public bool sound;
    private void Awake()
    {
        makesingolton();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        


    }
    public void SoundOnOff()
    {
        sound = !sound;
    }


    public void playSoundFx(AudioClip clip,float volume)
    {
        if (sound) {
        
        audioSource.PlayOneShot(clip, volume);
        
        }
    }
















}

