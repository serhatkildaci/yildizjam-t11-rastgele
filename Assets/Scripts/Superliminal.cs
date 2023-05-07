using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superliminal : MonoBehaviour
{
    public Transform target;

    public LayerMask targetMask;
    public LayerMask ignoreTargetMask;
    public float offsetFactor = 1;

    public float maxScale = 10f; // new field to control max scale

    float originalDistance;
    float originalScale;
    Vector3 targetScale;
    [SerializeField, Range(5f, 20f)] float pickUpDistance = 10f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleInput();
        ResizeTarget();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (target == null)
            {

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpDistance, targetMask))
                {
                    target = hit.transform;
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    originalDistance = Vector3.Distance(transform.position, target.position);
                    originalScale = target.localScale.x;
                    targetScale = target.localScale;
                    AudioManager.instance.Play("Grab");
                }
            }
            else
            {
                target.GetComponent<Rigidbody>().isKinematic = false;
                target = null;
            }
        }
    }

    void ResizeTarget()
    {
    if (target == null)
    {
        return;
    }

    RaycastHit hit;
    if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask)){
        Vector3 newPosition = hit.point - transform.forward * offsetFactor * (targetScale.x);

        // check for any obstacles in a spherical shape around the target's position
        float sphereRadius = target.localScale.x * 0.5f;
        Collider[] colliders = Physics.OverlapSphere(newPosition, sphereRadius, targetMask);
        bool isObstructed = false;
        foreach (Collider collider in colliders) {
            if (collider != target.GetComponent<Collider>()) {
                isObstructed = true;
                break;
            }
        }

        if (!isObstructed) {
            // check if the target is intersecting with any walls
            RaycastHit wallHit;
            if (Physics.Raycast(newPosition, -transform.forward, out wallHit, pickUpDistance, targetMask)) {
                newPosition = wallHit.point + transform.forward * sphereRadius;
            }

            target.position = newPosition;

            float currentDistance = Vector3.Distance(transform.position, target.position);
            float s = currentDistance / originalDistance;

            // limit scale to maxScale
            s = Mathf.Min(s, maxScale / originalScale);
            // limit scale to 1 (original size) or greater
            s = Mathf.Max(s, 1f);

            targetScale.x = targetScale.y = targetScale.z = s;

            target.transform.localScale = targetScale * originalScale;
        }
    }
    }
}
