using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 2;
    public float runSpeed = 6;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    //smooth variables
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;

    float currentSpeed;
    Animator animator;
	
	void Start () {
        //get the animator object
        animator = GetComponent<Animator> ();
	}
	
	void Update () {
        //user inputs
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        //for correct update of user input
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime) ;
        }
        bool running = Input.GetKey(KeyCode.LeftShift);
        //running
        float targetSpeed = ((running) ? runSpeed : walkSpeed)*inputDir.magnitude;

        //Move the user smoothly
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        transform.Translate(transform.forward * currentSpeed* Time.deltaTime, Space.World);
        
        //apply the animation
        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

	}
}
