using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Water : MonoBehaviour
{
    public Camera cam;
    public float WaterPowerToGive = 5;
    public float waterRange = 50;
    public ParticleSystem water;
    public AudioSource aud;

    public XRController xrController;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool selectButtonValue) && selectButtonValue)
        {
            water.Play();
            aud.Play();
            WaterUse();
        }

    }


    private void WaterUse()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, waterRange))
        {
            Debug.Log(hit.transform.name);
            Fire fireP = hit.transform.GetComponent<Fire>();

            if (fireP != null )
            {
                fireP.FirePowerTake(WaterPowerToGive);
            }




        }
    }
}
