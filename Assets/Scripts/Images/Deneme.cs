using UnityEngine;

public class Deneme : MonoBehaviour
{
    // Reference to the target object
    public Transform targetObject;

    // Reference to the trompe l'oeil object's mesh renderer
    private MeshRenderer meshRenderer;

    // Constant value used to determine the size of the trompe l'oeil object
    public float scaleFactor = 0.25f;

    void Start()
    {
        // Get a reference to the trompe l'oeil object's mesh renderer
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // Get the target object's world position and scale
        Vector3 targetPosition = targetObject.position;
        Vector3 targetScale = targetObject.localScale;

        // Calculate the distance and angle between the target object and the camera
        float distance = Vector3.Distance(transform.position, targetPosition);
        float angle = Vector3.Angle(transform.forward, targetPosition - transform.position);

        // Calculate the new scale based on the distance and angle
        float newScale = distance * Mathf.Tan(Mathf.Deg2Rad * angle) * scaleFactor;

        // Set the trompe l'oeil object's position and scale to match the target object's
        transform.position = targetPosition;
        transform.localScale = targetScale;

        // Set the trompe l'oeil object's new scale
        meshRenderer.material.SetFloat("_Scale", newScale);
    }
}
