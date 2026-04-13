using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - eg. references for readability or speed
    // STATE - private instances (member) variables
   [SerializeField] InputAction thrust;
   [SerializeField] InputAction rotation;
   [SerializeField] float  thrustStrenght= 100f ;
   [SerializeField] float rotationStrenght = 100f;
   [SerializeField] AudioClip mainEngineSFX ;
    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

  

   Rigidbody rb;
   AudioSource audioSource;
   //kleine (a) is de file en grote (S) is de type van de file
 

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        mainParticles.Play();
       
       
    }
   
    private void FixedUpdate()
    {
       ProcessThrust();
       ProcessRotation();
    }
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrenght * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                 audioSource.PlayOneShot(mainEngineSFX);
                 if (!mainParticles.isPlaying)
                {
                  mainParticles.Play();  
                }
                    
                    
                 
            }
           
        }    
               else
        {
            audioSource.Stop();
            mainParticles.Stop();
           
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput !=0)
        {
            if(rotationInput > 0){
                 ApplyRotation(-rotationStrenght);
                 
                 if (!rightParticles.isPlaying)
                {
                    leftParticles.Stop();
                 rightParticles.Play();  
                }
                 
            }
            else if(rotationInput < 0)
            {
                 ApplyRotation(rotationStrenght);
                 
                   if (!leftParticles.isPlaying)
                {
                    rightParticles.Stop();
                 leftParticles.Play();  
                }
            }
        }
        else
        {
                leftParticles.Stop();
                rightParticles.Stop();
        }
       
    }
    private void ApplyRotation(float rotationThisFrame)
    {
                    rb.freezeRotation = true; // freezing rotation so we can manually rotate        
                    transform.Rotate(Vector3.forward* rotationThisFrame * Time.deltaTime);
                    rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

   

}
