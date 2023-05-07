using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superliminal : MonoBehaviour
{
    public Transform target;

    public LayerMask targetMask;
    public LayerMask ignoreTargetMask;
    public float offsetFactor = 1;

    public float maxScale = 3f; // new field to control max scale

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
            target.position = hit.point - transform.forward * offsetFactor * (targetScale.x);

            // check if the target object is intersecting with any other colliders
            Collider[] colliders = Physics.OverlapBox(target.position, target.localScale / 2f, target.rotation, targetMask);
            if (colliders.Length > 0)
            {
                // move the target object away from the colliding objects
                foreach (Collider collider in colliders)
                {
                    Vector3 direction = (target.position - collider.ClosestPoint(target.position)).normalized;
                    target.position += direction * (collider.bounds.extents.magnitude + target.localScale.x / 2f);
                }
            }

            float currentDistance = Vector3.Distance(transform.position, target.position);
            float s = currentDistance / originalDistance;
            s = Mathf.Min(s, maxScale / originalScale); // limit scale to maxScale
            targetScale.x = targetScale.y = targetScale.z = s;

            target.transform.localScale = targetScale * originalScale;
        }
    }
}
