using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // Add this for event handling

public class Movement : MonoBehaviour
{
      public float force = 1200f;
      public float rotationSpeed = 500f;
    [SerializeField] private AudioClip boost;
    [SerializeField] private ParticleSystem leftThurster;
    [SerializeField] private ParticleSystem rightThurster;
    [SerializeField] private ParticleSystem booster;

    [SerializeField] private Button boosterButton; 
    [SerializeField] private Button leftButton;    
    [SerializeField] private Button rightButton; 

    private Rigidbody rb;
    private AudioSource au;
    private bool isBoosting = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

  void Start()
{
    rb = GetComponent<Rigidbody>();
    au = GetComponent<AudioSource>();

     // Load saved values from PlayerPrefs
    if (PlayerPrefs.HasKey("force"))
        force = PlayerPrefs.GetFloat("force");

    if (PlayerPrefs.HasKey("rotationSpeed"))
        rotationSpeed = PlayerPrefs.GetFloat("rotationSpeed");

    //Use EventTrigger to detect when button is pressed and released
    AddEvent(boosterButton, StartBooster, StopBooster);
    AddEvent(leftButton, StartLeftRotation, StopRotation);
    AddEvent(rightButton, StartRightRotation, StopRotation);
}

// Function to Add Event Listeners for Press and Release
void AddEvent(Button button, System.Action onPress, System.Action onRelease)
{
    EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { onPress(); });
    trigger.triggers.Add(pointerDown);

    EventTrigger.Entry pointerUp = new EventTrigger.Entry();
    pointerUp.eventID = EventTriggerType.PointerUp;
    pointerUp.callback.AddListener((eventData) => { onRelease(); });
    trigger.triggers.Add(pointerUp);
}

void Update()
{
    if (isBoosting) Startbooster();
    if (isRotatingLeft) Leftrotation();
    if (isRotatingRight) Rightrotation();
}

private void StartBooster() { isBoosting = true; }
private void StopBooster() { isBoosting = false; Stopbooster(); }

private void StartLeftRotation() { isRotatingLeft = true; }
private void StartRightRotation() { isRotatingRight = true; }
private void StopRotation() { isRotatingLeft = false; isRotatingRight = false; }


    private void Startbooster()
    {
        rb.AddRelativeForce(Vector3.up * force * Time.deltaTime);
        if (!au.isPlaying) au.PlayOneShot(boost);
        if (!booster.isPlaying) booster.Play();
    }

    private void Stopbooster()
    {
        au.Stop();
        booster.Stop();
    }

    private void Leftrotation()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThurster.isPlaying) leftThurster.Play();
    }

    private void Rightrotation()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThurster.isPlaying) rightThurster.Play();
    }

    private void ApplyRotation(float rotationSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
