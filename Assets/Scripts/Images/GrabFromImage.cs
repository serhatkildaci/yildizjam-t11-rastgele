using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFromImage : MonoBehaviour
{
    public GameObject obje;
    public GameObject[] destory;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Mouse0))
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Instantiate(obje, mousePosition, Quaternion.identity);
            for (int i = 0; i < destory.Length; i++)
            {
                Destroy(destory[i]);
            }
        }

    }

}


