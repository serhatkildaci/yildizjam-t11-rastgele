using UnityEngine;

public class HoldAndMoveObjects : MonoBehaviour
{
    public float objectDistance = 5f; // distance at which objects will be placed in front of camera
    public float maxReachDistance = 10f; // maximum distance the player can hold and move objects
    public LayerMask objectLayer; // layer(s) on which objects can be selected
    public float minY = -10f; // minimum y value at which objects can be placed
    public float minObjectDistance = 1f; // Add a minimum distance variable
    public float collisionResolution = 1f; // how much to move the object along the collision normal

    private GameObject heldObject = null; // currently held object
    private Vector3 heldObjectOffset = Vector3.zero; // offset between object's center and hit point
    private Vector3 lastValidPosition = Vector3.zero; // the last valid position of the held object

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxReachDistance, objectLayer))
            {
                AudioManager.instance.Play("Hold");
                heldObject = hit.collider.gameObject;
                heldObjectOffset = heldObject.transform.position - hit.point;
                lastValidPosition = heldObject.transform.position;
            }
        }

        if (Input.GetKey(KeyCode.E) && heldObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 newPosition = ray.origin + ray.direction * objectDistance + heldObjectOffset;
            newPosition.y = Mathf.Max(newPosition.y, minY); // prevent object from moving below minY

            // check for collisions and move the held object along the collision normal
            bool canMove = true;
            RaycastHit[] hits = Physics.RaycastAll(heldObject.transform.position, newPosition - heldObject.transform.position, (newPosition - heldObject.transform.position).magnitude, objectLayer);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != heldObject)
                {
                    Vector3 normal = hit.normal;
                    Vector3 direction = newPosition - heldObject.transform.position;
                    float distance = (newPosition - heldObject.transform.position).magnitude;
                    float objectExtent = heldObject.GetComponent<Renderer>().bounds.extents.magnitude;
                    float collisionDistance = (distance - objectExtent) + collisionResolution;
                    newPosition = hit.point + normal * collisionDistance + direction.normalized * collisionResolution;
                    canMove = false;
                }
            }

            float distanceToPlayer = Vector3.Distance(transform.position, newPosition);
            if (distanceToPlayer <= maxReachDistance && canMove)
            {
                heldObject.transform.position = newPosition;
                lastValidPosition = heldObject.transform.position;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            heldObject.transform.position = lastValidPosition;
            heldObject = null;
        }

        float rotateSpeed = 5.0f; // adjust as needed
        float rotationAmount = Input.mouseScrollDelta.y * rotateSpeed;

        if (heldObject != null)
        {
            Vector3 currentRotation = heldObject.transform.rotation.eulerAngles;
            heldObject.transform.rotation = Quaternion.Euler(0.0f, currentRotation.y + rotationAmount, 0.0f);
        }
    }
}
