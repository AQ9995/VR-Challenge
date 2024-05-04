using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Transform eyes;
    public float Spawndistance = 3;
    public GameObject menu;
    public InputActionProperty ShowMenu;
    public Slider slider;
    public AudioSource aud;
    public 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowMenu.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = eyes.position + new Vector3(eyes.forward.x, 0 , eyes.forward.z).normalized * Spawndistance;
        }
        menu.transform.LookAt(new Vector3(eyes.position.x, eyes.transform.position.y, eyes.position.z));
        menu.transform.forward *= -1;


        aud.volume = slider.value;
    }

    
}
