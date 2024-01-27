using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Closet : MonoBehaviour
{
    public GameObject massege;
    public float radius = 1;
    public XRController xrController;
    public Animator anim;
    public AudioSource aud;

    public GameObject ArrowHint1;
    public GameObject ArrowHint2;

    void Start()
    {
        massege.SetActive(false);
        ArrowHint1.SetActive(true);
        ArrowHint2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector3.Distance(transform.position,  xrController.transform.position) < radius )
        {
            massege.SetActive(true);
            ArrowHint1.SetActive(false);
            Debug.Log("Change!!!");

            if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool selectButtonValue) && selectButtonValue)
            {
                anim.SetBool("Play", true);
                aud.Play();
                Debug.Log("Anim Played");
                ArrowHint1.SetActive(false);
                ArrowHint2.SetActive(true);
            }
        }

        else
        {
            massege.SetActive(false);
        }
    }
}
