using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
[SerializeField]
GameObject door;
[SerializeField]
GameObject button;
bool isOpened = false;
    void OnTriggerEnter(Collider col)
    {
        Destroy(door);
    }
}
