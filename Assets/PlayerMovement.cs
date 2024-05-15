using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float walkSpeed = 4f;
  [SerializeField] private float maxVelocityChange = 10f;


  private Vector2 input;
  private Rigidbody rb;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    input.Normalize();
  }
  private void FixedUpdate()
  {
    rb.AddForce(CalculateMovement(walkSpeed), ForceMode.VelocityChange);
  }
  
  private Vector3 CalculateMovement(float _speed)
  {
    Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
    targetVelocity = transform.TransformDirection(targetVelocity);
    
    targetVelocity *= _speed;
    
    Vector3 velocity = rb.velocity;

    if (input.magnitude > 0.5f)
    {
      Vector3 velocityChange = targetVelocity - velocity;
      
      velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
      velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
      
      velocityChange.y = 0;
      
      return (velocityChange);
    }
    else
      return new Vector3();
    
  }


  

 
}
