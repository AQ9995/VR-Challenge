using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class pull : MonoBehaviour
{
    public XRLever lever;

    public float waterRange = 50;
    public float WaterPowerToGive = 5;
    [SerializeField] private float amountPerSecounds = 1f;

    //public Transform HoseNozzle;


    public ParticleSystem PS;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = HoseNozzle.position;
        if (lever.value == true)
        {
            PS.Play();
            WaterUse();
        }

        else
        {
            PS.Stop();
        }
    }


    private void WaterUse()
    {

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, waterRange)
            && hit.collider.TryGetComponent(out Fire fire))
        {

            if (fire != null)
            {
                fire.TryTakeDownFirePower(amountPerSecounds * Time.deltaTime);

            }
        }
    }
}
