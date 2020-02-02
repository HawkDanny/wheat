using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Animator anim;
    

    public Transform roadAxis;
    public float topSpeed;
    public float acceleration; //A small number that is added each frame the input is down
    public float deceleration;
    private Vector3 velocity;

    private void Start()
    {
        anim = GetComponent<Animator> ();
        velocity = Vector3.zero;
    }

    private void Update()
    {
        bool moving = false;
        if (Input.GetKey(KeyCode.W))
        {
            velocity += roadAxis.forward * acceleration * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity -= roadAxis.forward * acceleration * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += roadAxis.right * acceleration * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity -= roadAxis.right * acceleration * Time.deltaTime;
            moving = true;
        }
        
        if (!moving)
            velocity *= deceleration * Time.deltaTime;
        
        if (velocity.magnitude >= topSpeed)
        {
            velocity.Normalize();
            velocity *= topSpeed;
        }

        this.transform.Translate(velocity);

        if (velocity.magnitude > 0f)
        {
            Debug.Log(anim);
            anim.SetBool("isWalking", true);
            

        }
        else
        {
            anim.SetBool("isWalking", false);
            
        }
    }
}
