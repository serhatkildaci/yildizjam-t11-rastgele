using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // rotation speed in degrees per second
    public float floatSpeed = 0.5f; // float speed in units per second
    public float floatHeight = 0.5f; // maximum height to float above original position

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // rotate the object around its center on the y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // float the object up and down
        Vector3 floatPos = startPos;
        floatPos.y += Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = floatPos;
    }
}
