using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioClip[] musicClips = new AudioClip[0];
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicNext()
    {
       
    }

    public  void MusicPrev()
    {
         
    }
    
}
