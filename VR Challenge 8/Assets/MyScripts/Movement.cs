using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v  = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (h, 0, v);

        controller.Move (movement * speed * Time.deltaTime);

    }
}
