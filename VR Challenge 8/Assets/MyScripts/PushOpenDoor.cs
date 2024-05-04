using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushOpenDoor : MonoBehaviour
{

    public Animator anim;
    public string boolName = "open";
    public AudioSource aud;
    public GameObject arrowHint2;
    public GameObject arrowHint3;

    // Start is called before the first frame update
    void Start()
    {
        arrowHint3.SetActive(false);
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => ToggleDoorOpen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDoorOpen()
    {
        bool isOpen = anim.GetBool(boolName);
        anim.SetBool(boolName, !isOpen);
        aud.Play();
        arrowHint2.SetActive(false);
        arrowHint3.SetActive(true);
    }
}
