using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    //parameters
    [SerializeField] float mainThrust = 5f;
    [SerializeField] float rotationThrust = 5f;
    [SerializeField] AudioClip mainEngine; 

    //cache
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }

    // hold space key down to move rocket along y axis (frame rate independent)
    // plays audio when key is held if no audio source is currently playing
    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Thrusting");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }

    }


    // hold down input keys to rotate forward or backwards along z axis
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ForwardRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            BackwardsRotation(rotationThrust);
        }
        
    }

    // rotate forward along z axis
    // turn off/on rigid body freeze rotation
    // frame rate independent
    void ForwardRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over;
    }

    // rotate backwards along z axis
    // turn off/on rigid body freeze rotation
    // frame rate independent
    void BackwardsRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(Vector3.back * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over;
    }
}
