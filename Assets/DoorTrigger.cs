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
        if(!isOpened){
        door.transform.position += new Vector3(0,20,0);
        button.transform.position += new Vector3(0, -0.5f,0); 
        isOpened=true;
        }
    }
}
