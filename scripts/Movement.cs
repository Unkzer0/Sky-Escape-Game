using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class   Movement : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioClip boost;
    [SerializeField] private ParticleSystem leftThurster;
    [SerializeField] private ParticleSystem rightThurster;
    [SerializeField] private ParticleSystem booster;


    Rigidbody rb;
    AudioSource au;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        RocketBoost(); 
        RocketRotate();
    }

    void RocketBoost()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Startbooster();
        }
        else
        {
            Stopbooster();
        }
    }

    void RocketRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rightrotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Leftrotation();
        }
        else
        {
            Stoprotation();
        }
    }

    private void Startbooster()
    {
        rb.AddRelativeForce(Vector3.up * force * Time.deltaTime);
        if (!au.isPlaying)
        {
            au.PlayOneShot(boost);
        }
        if (!booster.isPlaying)
        {
            booster.Play();
        }
    }
    private void Stopbooster()
    {
        au.Stop();
        booster.Stop();
    }
    private void Leftrotation()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThurster.isPlaying)
        {
            leftThurster.Play();
        }
    }

    private void Rightrotation()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThurster.isPlaying)
        {
            rightThurster.Play();
        }
    }
    private void Stoprotation()
    {
        rightThurster.Stop();
        leftThurster.Stop();
    }

    private void ApplyRotation(float rotationSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false;

    }
}
