using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
   [SerializeField] InputAction thrust;
   [SerializeField] InputAction rotation;
   [SerializeField] float  thrustStrenght= 100f ;
   [SerializeField] float rotationStrenght = 100f;
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
                 audioSource.Play();
            }
           
        }    
               else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput !=0)
        {
            if(rotationInput > 0){
                 ApplyRotation(-rotationStrenght);
            }
            else if(rotationInput < 0)
            {
                 ApplyRotation(rotationStrenght);
            }
        }
       
    }
    private void ApplyRotation(float rotationThisFrame)
    {
                    rb.freezeRotation = true; // freezing rotation so we can manually rotate        
                    transform.Rotate(Vector3.forward* rotationThisFrame * Time.deltaTime);
                    rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

   

}
