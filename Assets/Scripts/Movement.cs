using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX; // Ensure you have the Input System package installed

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("InputActions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header("Component Referewnces")]
    [SerializeField] private Rigidbody rb; // Reference to Rigidbody component
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxthrust;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem sfxmain;
    [SerializeField] private ParticleSystem sfxleft;
    [SerializeField] private ParticleSystem sfxright;


    [Header("Power Values")]
    [SerializeField] private float thrustPower;
    [SerializeField] private float rotationPower;

    
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
      
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize the Rigidbody reference
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
    }

    private void Update()
    {
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationvalue = rotation.ReadValue<float>();
        if (rotationvalue !=0)
        {
            rb.freezeRotation = true; // Temporarily freeze physics rotation
            transform.Rotate(rotationvalue * -Vector3.forward * rotationPower * Time.deltaTime); // Simplified rotation handling
            rb.freezeRotation = false; // Unfreeze physics rotation
        }
        else
        {
            sfxleft.Stop();
            sfxright.Stop();
        }

        if (rotationvalue == -1)
        {
            sfxleft.Play();
        }
        if(rotationvalue == 1)
        {
            sfxright.Play();
        }

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            if (!audioSource.isPlaying) // Play sound only if it's not already playing
            {
                audioSource.PlayOneShot(sfxthrust);
            }
            if(!sfxmain.isPlaying)
            {
                sfxmain.Play();
            }
            //Debug.Log("Thrusting!");
            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
            sfxmain.Stop();
        }

    }
}
