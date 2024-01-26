using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GetTheCar : MonoBehaviour
{
    public GameObject massege;
    public float radius = 1;
    public XRController xrController;
    public Animator anim;
    public AudioSource aud;

    public int time = 7;

    public GameObject xrOrign;
    public GameObject car;
    void Start()
    {
        massege.SetActive(false);
        //StartCoroutine(ArriveTOFire());
    }

    // Update is called once per frame
    void Update()
    {


        

        if (Vector3.Distance(transform.position, xrController.transform.position) < radius)
        {
            massege.SetActive(true);
            Debug.Log("Change!!!");

            if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool selectButtonValue) && selectButtonValue)
            {
                anim.SetBool("Play", true);
                aud.Play();
                StartCoroutine(ArriveTOFire());
                Debug.Log("Anim Played");
            }
        }

        else
        {
            massege.SetActive(false);
        }
    }


    IEnumerator ArriveTOFire()
    {

        yield return new WaitForSeconds(time);
        car.transform.position  = new Vector3 (1.75f, -0.51f, 3.03f);
        car.transform.Rotate(0, 49f, 0);
        xrOrign.transform.position = new Vector3(7.04f, 1, 1.63f);
        xrOrign.transform.Rotate(0, 188.589f, 0);
    }
}
