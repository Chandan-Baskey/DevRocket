using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip sfxCrash;
    [SerializeField] AudioClip sfxFinish;
    [SerializeField] private ParticleSystem vfxCrash;
    [SerializeField] private ParticleSystem vfxFinish;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] float delayTime = 2f;
    bool isControllable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckDebugKeys();
        CheckForQuit();
    }

    private static void CheckForQuit()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) // Quit the application
        {
            Application.Quit();
        }
    }

    private void CheckDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isControllable = !isControllable; // Toggle collision handling
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isControllable) return; // Prevent multiple collision handling
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly object.");
                break;
            case "Finish":
                Debug.Log("Collided with Finish object.");
                StartFinishSquence();
                break;
                
            default:
                Debug.Log("Collided with an obstacle.");
                StartCrashSquence();
                break;

        } 
    }
    private void StartFinishSquence() 
    {
        isControllable = false; 
        vfxFinish.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(sfxFinish);
        GetComponent<NewMonoBehaviourScript>().enabled = false; // Disable player controls
        Invoke("LoadNextScene", delayTime); // Delay load by 1 second
    }
    private void StartCrashSquence()
    {
        isControllable = true;
        vfxCrash.Play();
        audioSource.PlayOneShot(sfxCrash);
        GetComponent<NewMonoBehaviourScript>().enabled = false; // Disable player controls
        Invoke("ReloadScene", delayTime); // Delay reload by 1 second 
    }
    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index
        int nextIndex = currentIndex + 1;
        if (currentIndex == SceneManager.sceneCountInBuildSettings - 1) // If it's the last scene
        {
            nextIndex = 2; // Loop back to the first scene if at the last scene
        }
        SceneManager.LoadScene(nextIndex); // Reload the current scene
    }
    private void ReloadScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex; // Get the current scene index
        SceneManager.LoadScene(currentIndex); // Reload the current scene
    }
}
