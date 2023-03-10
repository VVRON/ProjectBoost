using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 5f;
    [SerializeField] float rotationThrust = 5f;

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

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Thrusting");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

    }

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

    void ForwardRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over;
    }

    void BackwardsRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(Vector3.back * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over;
    }
}
