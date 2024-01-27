using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Canvas menuCanvas;
 
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
        
        Time.timeScale = 1.0f;

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exit");
    } 


 
}
