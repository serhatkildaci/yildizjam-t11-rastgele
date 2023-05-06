using UnityEngine;

public class HoldAndMoveObjects : MonoBehaviour
{
    public float objectDistance = 5f; // distance at which objects will be placed in front of camera
    public LayerMask objectLayer; // layer(s) on which objects can be selected
    public float minY = -10f; // minimum y value at which objects can be placed
    public float minObjectDistance = 1f; // Add a minimum distance variable

    private GameObject heldObject = null; // currently held object
    private Vector3 heldObjectOffset = Vector3.zero; // offset between object's center and hit point

    void Update()
    {
        objectDistance += Input.mouseScrollDelta.y;

        if (objectDistance < minObjectDistance)
        {
            objectDistance = minObjectDistance;
        }

        if (Input.GetKey(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectLayer))
            {
                heldObject = hit.collider.gameObject;
                heldObjectOffset = heldObject.transform.position - hit.point;
            }
        }

        if (Input.GetKey(KeyCode.E) && heldObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 newPosition = ray.origin + ray.direction * objectDistance + heldObjectOffset;
            newPosition.y = Mathf.Max(newPosition.y, minY); // prevent object from moving below minY
            heldObject.transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            heldObject = null;
        }
    
    }
}
