using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5.0f;
    private Vector3 moveVector;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 3.0f;
    private bool isdead = false;
    private float Starttime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Starttime = Time.time;
    }

   
    void Update()
    {
        if (isdead)
            return;
        if(Time.time - Starttime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = 15.0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // x = right left
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetMouseButtonDown(0))
        {
            if ((Input.mousePosition.x > Screen.width / 2) && (Input.mousePosition.y < Screen.height / 2))
                moveVector.x = speed+50f;
            if ((Input.mousePosition.x < Screen.width / 2) && (Input.mousePosition.y < Screen.height / 2))
                moveVector.x = -speed-50f;    
        }
        
        // y = up down
        moveVector.y = Input.GetAxisRaw("Jump") * verticalVelocity;
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y > Screen.height / 2)
                moveVector.y = verticalVelocity + 125f;
        }

        // z = forward backword
        moveVector.z = speed;


        controller.Move(moveVector * Time.deltaTime);
    }

    public void setspeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        //if (hit.point.z > transform.position.z + controller.radius)
        if(hit.gameObject.tag == "Enemy")
            Death();
    }

    private void Death()
    {
        isdead = true;
        GetComponent<Score>().Ondeath();
    }
}
