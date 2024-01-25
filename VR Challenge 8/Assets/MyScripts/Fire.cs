using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    [SerializeField, Range(0f,1f)] private float currentfirePower = 1.0f;
    private float[] startIntensities = new float[0];
    [SerializeField] private ParticleSystem[] fireParticles = new ParticleSystem[0];

    [SerializeField] private float timeLastWatered = 0;
    [SerializeField] private float regenDelay = 2.5f;
    [SerializeField] private float regenRate = .1f;

    public ParticleSystem steam;

    private bool isFire = true;
   
    void Start()
    {
        startIntensities = new float[fireParticles.Length];

        for (int i = 0; i < fireParticles.Length; i++)
        {
            startIntensities[i] = fireParticles[i].emission.rateOverTime.constant;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (isFire && currentfirePower < 1.0f && Time.time - timeLastWatered >= regenDelay)
        {
            currentfirePower += regenRate * Time.deltaTime;
            ChangeIntensity();
        }
    }

    public void ChangeIntensity()
    {

        for (int i = 0; i < fireParticles.Length; i++)
        {
            var emission = fireParticles[i].emission;
            emission.rateOverTime = currentfirePower * startIntensities[i];
        }
        
    }

    public bool TryTakeDownFirePower(float amount)
    {
        timeLastWatered = Time.time;
        currentfirePower -= amount;
        ChangeIntensity();

        if (currentfirePower < 0.0f)
        {
            isFire = false;
            steam.Play();
            return true;
        }
        

            return false;
    }
}
