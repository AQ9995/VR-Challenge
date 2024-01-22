using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public float firePower = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FirePowerTake(float power)
    {
        firePower -= power;

        if( firePower <= 0)
        {
            Destroy(gameObject);
        }
        
        //setFalse to fire if == 0
    }
}
