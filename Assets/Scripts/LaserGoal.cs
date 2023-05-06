using UnityEngine;

public class LaserGoal : MonoBehaviour
{
    public GameObject doorToBeOpened;
    public bool trigger = false;
    public float doorOpenDistance = 100f; // adjust this value to control the distance the door opens

    private bool doorIsOpen = false;
    private Vector3 doorInitialPosition;

    void Start()
    {
        doorInitialPosition = doorToBeOpened.transform.position;
        InvokeRepeating("TriggerChange", 0, 0.2f);
    }

    void Update()
    {
        if (trigger && !doorIsOpen)
        {
            float newY = doorInitialPosition.y + doorOpenDistance;
            doorToBeOpened.transform.position = new Vector3(doorToBeOpened.transform.position.x, Mathf.Lerp(doorInitialPosition.y, newY, 0.01f), doorToBeOpened.transform.position.z);
            if (doorToBeOpened.transform.position.y >= newY)
            {
                doorIsOpen = true;
            }
        }
        else{
            doorToBeOpened.transform.position = new Vector3(doorToBeOpened.transform.position.x, Mathf.Lerp(transform.position.y, doorInitialPosition.y, 0.01f), doorToBeOpened.transform.position.z);
            doorIsOpen = false;
        }
    }

    void TriggerChange(){
        trigger = false;
    }

}
