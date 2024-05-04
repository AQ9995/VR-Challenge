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
   

    public Material[] mat;

   

    public SkinnedMeshRenderer rendL;
    public SkinnedMeshRenderer rendR; 

    void Start()
    {
        massege.SetActive(false);
        ArrowHint1.SetActive(true);
        ArrowHint2.SetActive(false);
        rendL.enabled = true;
        rendR.enabled = true;
        rendL.sharedMaterial = mat[0];
        rendR.sharedMaterial = mat[0];



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
                //mat.color = Color.yellow;
                rendL.sharedMaterial = mat[1];
                rendR.sharedMaterial = mat[1];
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
