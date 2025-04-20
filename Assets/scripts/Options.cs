using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume",volume);
    }

    public void SetRotation(float rotation)
    {
      PlayerPrefs.SetFloat("rotationSpeed", rotation);
    }
    public void SetForce(float force)
    {
      PlayerPrefs.SetFloat("force", force);
    }    

}
