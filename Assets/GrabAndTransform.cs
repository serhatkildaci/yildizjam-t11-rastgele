using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndTransform : MonoBehaviour
{
    // The 2D object to grab
    public GameObject objectToGrab;

    // The 3D object to transform into
    public GameObject transformedObject;

    // Whether the object is currently being held or not
    private bool isHeld = false;

    // The position and rotation of the 2D object before it was grabbed
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    // The Rigidbody2D and Rigidbody components of the 2D and 3D objects, respectively
    private Rigidbody2D objectToGrabRb;
    private Rigidbody transformedObjectRb;

    // The force multiplier to apply when moving the 3D object
    public float moveForceMultiplier = 10f;

    // The player's movement input
    private float moveInput;

    private void Start()
    {
        // Get the Rigidbody2D and Rigidbody components of the objects
        objectToGrabRb = objectToGrab.GetComponent<Rigidbody2D>();
        transformedObjectRb = transformedObject.GetComponent<Rigidbody>();

        // Record the original position and rotation of the 2D object
        originalPosition = objectToGrab.transform.position;
        originalRotation = objectToGrab.transform.rotation;

        // Disable the Rigidbody component on the 3D object at the start
        transformedObjectRb.isKinematic = true;
    }

    private void Update()
    {
        // Get the player's movement input
        moveInput = Input.GetAxis("Horizontal");

        if (isHeld)
        {
            // Move the 3D object with the player's movement input
            transformedObjectRb.AddForce(Vector3.right * moveInput * moveForceMultiplier);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object being collided with is the player and if the object has not already been grabbed
        if (other.gameObject.CompareTag("Player") && !isHeld)
        {
            // Disable the Rigidbody2D component on the 2D object
            objectToGrabRb.isKinematic = true;

            // Enable the Rigidbody component on the 3D object
            transformedObjectRb.isKinematic = false;

            // Set the position and rotation of the 3D object to match that of the 2D object
            transformedObject.transform.position = objectToGrab.transform.position;
            transformedObject.transform.rotation = objectToGrab.transform.rotation;

            // Mark the object as being held
            isHeld = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object being collided with is the player and if the object has already been grabbed
        if (other.gameObject.CompareTag("Player") && isHeld)
        {
            // Disable the Rigidbody component on the 3D object
            transformedObjectRb.isKinematic = true;

            // Enable the Rigidbody2D component on the 2D object
            objectToGrabRb.isKinematic = false;

            // Reset the position and rotation of the 2D object to its original values
            objectToGrab.transform.position = originalPosition;
            objectToGrab.transform.rotation = originalRotation;

            // Mark the object as no longer being held
            isHeld = false;
        }
    }
}
