using UnityEngine;
using System.Collections;
 
/* THIRD PERSON CAMERA: FOLLOW + ORBIT AROUND FOCUSING CHARACTER */
/* Jump with Scroll Wheel*/
public class ThirdPersonCamera : MonoBehaviour {
    // The Container: Rotation Pivot
    Transform pivot;
    Transform player;
    Camera self;
    // How Fast or Slow is the Orbitting Rate
    public float sensitivity;
    // Vertical Degrees Limits
    public float bottomLimit = 0f, topLimit = 90f;
    public float orbitDamping;
    public float zoomMin = 20f, zoomMax = 80f;
    public float zoomRate;
    // Storing the Current Rotation
    Vector3 localRotation;
    float zoom;

    void Start() {
        pivot = transform.parent;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        // Default Zoom
        self = GetComponent<Camera>();
        zoom = self.fieldOfView;
    }

    // Follow after all Update Calculations have been processed
    void LateUpdate() {
        Follow();
        Orbit();
        Zoom();
    }

    // Pivot Follow Player
    void Follow() {
        pivot.position = player.position;
    }

    // Rotate Pivot Container with Mouse Movements around to Orbit Camera
    void Orbit() {
        // Set rotation by the mouse Moving Coordinates
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            // Adding instead of Assigning: Keeping the Last Rotation while Accumulating  Movements
            localRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            // opposite Direction for Vertical Movements
            localRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            // Clamping Vertical Rotation: Above Ground and won't be Flipped upside down
            localRotation.y = Mathf.Clamp(localRotation.y, bottomLimit, topLimit);
        }

        // Set Pivot Rotation
        Quaternion rotation = Quaternion.Euler(localRotation.y, localRotation.x, 0f);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, rotation, orbitDamping * Time.deltaTime);
    }

    void Zoom() {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomRate;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        // Change Field of View
        self.fieldOfView = zoom;
    }
}
