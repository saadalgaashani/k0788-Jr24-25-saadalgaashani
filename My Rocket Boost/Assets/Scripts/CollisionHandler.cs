using System;
using UnityEngine;
using UnityEngine.SceneManagement;

   

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
     [SerializeField] AudioClip crashes;
   [SerializeField] AudioClip success;
   [SerializeField] ParticleSystem crashParticles;
   [SerializeField] ParticleSystem successParticles;
    AudioSource audioSource; 
    bool iscontrollable = true;



     private void Start()
    {
                audioSource = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter(Collision other)
    {
        if (!iscontrollable) {  return;    }
      switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are on the start line");
                break;
            case "Finish":
                StartSuccessSequence();
                
                break;
            
            default:
            StartCrashSequence();
            
                break;
        }
    }
  


    private void StartSuccessSequence()
    {
        // too add sfx and particles
        iscontrollable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();

      GetComponent<Movement> ().enabled = false;
        
        Invoke("LoadNextLevel" , levelLoadDelay);   
     }

    void StartCrashSequence()
    {
       // too add sfx and particles
       iscontrollable = false;
        audioSource.Stop();
         audioSource.PlayOneShot(crashes);
        crashParticles.Play();
        GetComponent<Movement> ().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
                {
          int currentScene = SceneManager.GetActiveScene().buildIndex;
          int nextScene = currentScene + 1;
         if (nextScene == SceneManager.sceneCountInBuildSettings)
   { nextScene = 0; }
    
    SceneManager.LoadScene(nextScene);
      }
                
    void ReloadLevel()
      {
      int currentScene = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentScene);
      }
}
