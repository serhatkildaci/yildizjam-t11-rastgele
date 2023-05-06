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

    void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        HandleInput();
        ResizeTarget();
    }

    void HandleInput(){
        if(Input.GetMouseButtonDown(0)){
            if(target == null){
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask)){
                    target = hit.transform;
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    originalDistance = Vector3.Distance(transform.position, target.position);
                    originalScale = target.localScale.x;
                    targetScale = target.localScale;
                }
            }
            else{
                target.GetComponent<Rigidbody>().isKinematic = false;
                target = null;
            }
        }
    }

    void ResizeTarget(){
        if(target == null){
            return;
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask)){
            target.position = hit.point - transform.forward * offsetFactor * (targetScale.x);

            float currentDistance = Vector3.Distance(transform.position, target.position);
            float s = currentDistance / originalDistance;
            s = Mathf.Min(s, maxScale / originalScale); // limit scale to maxScale
            targetScale.x = targetScale.y = targetScale.z = s;

            target.transform.localScale = targetScale * originalScale;
        }
    }
}
