using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
   [SerializeField] InputAction thrust;
   [SerializeField] InputAction rotation;
   [SerializeField] float  thrustStrenght= 100f ;
   [SerializeField] float rotationStrenght = 100f;
   Rigidbody rb;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
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
                    transform.Rotate(Vector3.forward* rotationThisFrame * Time.deltaTime);

    }
}
