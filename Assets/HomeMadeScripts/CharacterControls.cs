using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class CharacterControls : MonoBehaviour
{

    public float speed;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 14.0f;
    private bool grounded = false;
    public Rigidbody r;
    void Awake()
    {
        r.freezeRotation = true;
        r.useGravity = false;
    }

    void FixedUpdate()
    {


        Vector3 velocity = new Vector3(0, 0, 0);
        Vector3 velocityChange = new Vector3(0, 0, 0);
            if (grounded)
            {
                // Calculate how fast we should be moving
                Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);


                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    speed *= 10;

                    targetVelocity *= speed;


                    // Apply a force that attempts to reach our target velocity
                    velocity = r.velocity;
                    velocityChange = (targetVelocity - velocity);
                    speed /= 10;
                }
                else
                {
                    targetVelocity *= speed;


                    // Apply a force that attempts to reach our target velocity
                    velocity = r.velocity;
                    velocityChange = (targetVelocity - velocity);

                }



                if (canJump && Input.GetButton("Jump"))
                {
                    r.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }
                else
                {
                    r.AddForce(velocityChange, ForceMode.VelocityChange);
                }



            }

            // We apply gravity manually for more tuning control
            r.AddForce(new Vector3(velocity.x, -gravity * r.mass, velocity.z));

            grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}