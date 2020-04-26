using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* REQUIREMENT */
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

/* RIGIDBODY-BASED THIRD PERSON CHARACTER CONTROLLER */
/* Integrated with Animations*/
public class ThirdPersonController : MonoBehaviour
{
    /* REFERENCES */
    Rigidbody rgbd;
    Animator animator;

    /* PROPERTIES */
    bool grounded = false;

    [Header("Movement Properties")]
    public Camera followingCamera;
    public float speed = 10f;
    Vector3 targetVelocity = Vector3.zero;
    Vector3 addingVelocity = Vector3.zero;
    Vector3 rotationVelocity = Vector3.zero;
    // Saving the maintaining Local Rotation
    Quaternion lastRotation = Quaternion.identity;

    [Header("Jumping Properties")]
    public float jumpHeight = 2f;
    // We apply Graity manually
    public float gravity = 10f;
    // How fast will Fall down
    public float dropForce = 1.5f;
    // Times that you can Jump from Grounded State
    public int maxJump = 1;
    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        // Setting ups
        rgbd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rgbd.useGravity = false;
        jumpCount = maxJump;
    }

    // Physics Related Operations
    void FixedUpdate() {
        if (grounded) {
            Move();
            HandleRotation();
        }
        Jump();
        Fall();
        Gravity();
    }

     void OnCollisionEnter(Collision collision) {
        // Ground check
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
            // Reset Jump State
            animator.SetBool("Jumping", false);
            jumpCount = maxJump;
        }
    }

    private void OnCollisionExit(Collision collision) {
        // Out of Ground
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = false;
        }
    }

    // Movement Relative to Camera Transform
    void Move() {
        // GEtting Input Axes
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        // Flaten + Normalized Relative Camera Directions
        Vector3 camForward = followingCamera.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();
        Vector3 camRight = followingCamera.transform.right;
        camRight.y = 0f;
        camRight.Normalize();
        // Target Velocity relative to Player-Camera Direction
        targetVelocity = camForward * verticalAxis + camRight * horizontalAxis;
        // Speed Modifier
        targetVelocity *= speed;
        // Calculate Adding Force: Clamping to Target Velocity
        addingVelocity = targetVelocity - rgbd.velocity;
        addingVelocity.x = Mathf.Clamp(addingVelocity.x, -speed, speed);
        addingVelocity.z = Mathf.Clamp(addingVelocity.z, -speed, speed);
        addingVelocity.y = 0f;
        // Update the Movement over time with the Calculated Velocity
        rgbd.AddForce(addingVelocity, ForceMode.VelocityChange);
        // Animations on Moving
        if (rgbd.velocity.x != 0f || rgbd.velocity.z != 0f) {
            animator.SetBool("Moving", true);
        } else {
            animator.SetBool("Moving", false);
        }
    }

    // Handle Rotations on Movement
    void HandleRotation() {
        rotationVelocity = rgbd.velocity;
        rotationVelocity.y = 0f;
        // Only Rotate if there is movement registered
         if (rotationVelocity != Vector3.zero) {
            rgbd.rotation = Quaternion.LookRotation(rotationVelocity);
            lastRotation = rgbd.rotation;
        } else {
            // Keep Rotation still if the character is not moving
            rgbd.rotation = lastRotation;
        }
    }

    // Jump on Input
    void Jump() {
        if (jumpCount > 0 && Input.GetButtonDown("Jump")) {
            rgbd.AddForce(new Vector3(0, JumpSpeed(), 0), ForceMode.VelocityChange);
            // Animation
            animator.SetBool("Moving", false);
            animator.SetBool("Jumping", true);
            // No more Jump
            jumpCount--;
        }
    }

    void Fall() {
        // Falling Animation
        if (animator.GetBool("Jumping") && rgbd.velocity.y < 0f) {
            animator.SetTrigger("Fall");
        }
    }

    // Continous Gravity
    void Gravity() {
        // Manually Apply Gravity until Upward Velocity returns to 0
        rgbd.AddForce(new Vector3(0, -gravity * dropForce, 0));
    }

    // Calculate Upward Jumping force to reach Apex Height
    float JumpSpeed() {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
