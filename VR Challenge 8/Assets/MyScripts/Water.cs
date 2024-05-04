using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Water : MonoBehaviour
{

    [SerializeField] private float amountPerSecounds = 1f;
    //public Camera cam;
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

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, waterRange)
            && hit.collider.TryGetComponent(out Fire fire))
        {
            
            if (fire != null )
            {
                fire.TryTakeDownFirePower(amountPerSecounds * Time.deltaTime) ;
                
            }
        }
    }
}
