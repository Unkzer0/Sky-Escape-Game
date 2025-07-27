using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public float force = 1500f;
    public float rotationSpeed = 700f;

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

        // Use default values if not found
        force = PlayerPrefs.GetFloat("force", 1500f);
        rotationSpeed = PlayerPrefs.GetFloat("rotationSpeed", 700f);

        Debug.Log("Loaded Force: " + force + ", RotationSpeed: " + rotationSpeed);

        AddEvent(boosterButton, StartBooster, StopBooster);
        AddEvent(leftButton, StartLeftRotation, StopRotation);
        AddEvent(rightButton, StartRightRotation, StopRotation);
    }

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

    void FixedUpdate()
    {
        if (isBoosting) ApplyBoost();
        if (isRotatingLeft) RotateLeft();
        if (isRotatingRight) RotateRight();
    }

    private void StartBooster() { isBoosting = true; }
    private void StopBooster() { isBoosting = false; StopBoostEffects(); }

    private void StartLeftRotation() { isRotatingLeft = true; }
    private void StartRightRotation() { isRotatingRight = true; }
    private void StopRotation() { isRotatingLeft = false; isRotatingRight = false; }

    private void ApplyBoost()
    {
        rb.AddRelativeForce(Vector3.up * force * Time.fixedDeltaTime);
        if (!au.isPlaying) au.PlayOneShot(boost);
        if (!booster.isPlaying) booster.Play();
    }

    private void StopBoostEffects()
    {
        au.Stop();
        booster.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThurster.isPlaying) leftThurster.Play();
    }

    private void RotateRight()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThurster.isPlaying) rightThurster.Play();
    }

    private void ApplyRotation(float rotSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotSpeed * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
