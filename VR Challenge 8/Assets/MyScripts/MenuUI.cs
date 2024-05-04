using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Canvas menuCanvas;
    public AudioSource aud;
    public GameObject RayL;
    public GameObject RayR;

 
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void PlayGame()
    {
       menuCanvas.enabled = false;
        RayL.SetActive(false);
        RayR.SetActive(false);
        
        
        Time.timeScale = 1.0f;

        aud.Play();

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit");
    } 


 
}
