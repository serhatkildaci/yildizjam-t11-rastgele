using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // rotation speed in degrees per second

    void Update()
    {
        // rotate the object around its center on the y-axis
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
}
