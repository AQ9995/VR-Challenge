
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AudioPlay : MonoBehaviour
{

    public AudioSource aud;

    private XRController xrController;

    // Start is called before the first frame update
    void Start()
    {
        xrController = GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {

        // if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool selectButtonValue) && selectButtonValue)
        if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool selectButtonValue) && selectButtonValue)
        {
            aud.Play();
        }


    }
}
