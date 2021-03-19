using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMS - Visible and adjustable in Unity UI
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1000f;
    [SerializeField] AudioClip mainEngine;

    // CACHE - Variables to assist readability
    Rigidbody rb;
    AudioSource rocketThrusterSound;

    // STATE - Private instance (member) variables

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketThrusterSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!rocketThrusterSound.isPlaying)
            {
                rocketThrusterSound.PlayOneShot(mainEngine);
            }
        }
        else
        {
            rocketThrusterSound.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
