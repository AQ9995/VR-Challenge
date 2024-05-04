using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class treeFalling : MonoBehaviour
{
    public XRController xrOrigin;

    public float distance = 10;
    public Animator anim;
    public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, xrOrigin.transform.position) < distance)
        {
            anim.SetBool("fall", true);
            aud.Play();
        }
    }
}
