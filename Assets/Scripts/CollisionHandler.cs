using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) { return; }

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("A friendly item");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), delay);

        isTransitioning = false;
    }

    void StartCrashSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), delay);
        
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
        
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
