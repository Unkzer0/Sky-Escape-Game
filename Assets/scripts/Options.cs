using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
        Debug.Log("Volume set to: " + volume);
    }

    public void SetRotation(float rotation)
    {
        rotation = Mathf.Clamp(rotation, 100f, 1500f);
        PlayerPrefs.SetFloat("rotationSpeed", rotation);
        Debug.Log("Rotation speed set to: " + rotation);
    }

    public void SetForce(float force)
    {
        force = Mathf.Clamp(force, 500f, 3000f);
        PlayerPrefs.SetFloat("force", force);
        Debug.Log("Force set to: " + force);
    }
}
