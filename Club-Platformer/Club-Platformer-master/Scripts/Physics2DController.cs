using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Physics2DController : MonoBehaviour {
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    
    float gravity = -20;

    public float moveSpeed = 6;
    Vector3 velocity;
    float velocityXSmoothing;

    public float targetVelocityX = 0f;

    Controller2D controller;

    void Awake()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);

        print("Gravity: " + gravity);
    }
    
	void Update () {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (controller.collisions.right || controller.collisions.left)
        {
            velocity.x = 0;
        }

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //if (velocity.x != 0)
        //{
            //velocity.x = Mathf.SmoothDamp(velocity.x, 0f, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //}
    }
}
