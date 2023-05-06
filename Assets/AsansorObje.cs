using UnityEngine;

public class AsansorObje : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; // direction of movement
    public float moveDistance = 5f; // distance to move in each direction
    public float moveSpeed = 2f; // speed of movement

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        // determine the target position for this frame
        Vector3 targetPos = movingToEnd ? endPos : startPos;

        // move the object towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // if we've reached the target position, switch direction
        if (transform.position == targetPos)
        {
            movingToEnd = !movingToEnd;
        }
    }
}
