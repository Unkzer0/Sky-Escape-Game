
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] private float delay = 2f;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private ParticleSystem crashParticals;
    [SerializeField] private ParticleSystem levelParticals;
    private AudioSource audioSource;
    bool collisionDisabled = false;
    bool isTransitioning = false;
    Score scoreManager;
        

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreManager = FindAnyObjectByType<Score>();
    }

    void Update()
    {
        Cheat();
    }
    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    private void Reloadlevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
     void LoadNextLevel()
    {
        isTransitioning = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentSceneIndex + 1;
       if(nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        }
       SceneManager.LoadScene(nextIndex);
    }
    private void Gameloading()
    {
        isTransitioning = true;
        GetComponent<AudioSource>().Stop();
        levelParticals.Play();
        GetComponent<AudioSource>().PlayOneShot(levelUpSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }
    private void Crashsquence()
    {
        isTransitioning = true;
        GetComponent<AudioSource>().Stop();
        crashParticals.Play();
        GetComponent<AudioSource>().PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("Reloadlevel", delay);
    }

    private void Hitpoint()
    {
        scoreManager.Updatescore(10);
    }

    private void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.tag.Equals("Fuel"))
        {
            Hitpoint();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {  if (isTransitioning || collisionDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Launch":
                Debug.Log("launching");
                break;
            case "Friendly":
                Debug.Log("friendly");
                break;

          case "Land":
                Debug.Log("level up!!");
                Gameloading();
                 break;

            default:
                Debug.Log("Rocket blow up!!");
                Crashsquence();
                break;
        }
    }

}
