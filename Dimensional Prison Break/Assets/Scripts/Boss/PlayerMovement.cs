﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float w = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }

        Move(h, w);
        //Turning();
        Animating(h, w);
    }

    void Move(float h,float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(transform.position + movement);

        Vector3 playerToMouse = movement;
        playerToMouse.y = 0f;

        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

        playerRigidbody.MoveRotation(newRotation);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay,out floorHit, camRayLength, floorMask))
        {

        }
    }

    void Animating(float h,float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
